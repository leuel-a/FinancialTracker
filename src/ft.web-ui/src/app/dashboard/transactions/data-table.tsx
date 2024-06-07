'use client'

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from '@/components/ui/table'
import {
  ColumnFiltersState,
  VisibilityState,
  ColumnDef,
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  getFilteredRowModel,
  useReactTable,
  SortingState,
  getSortedRowModel
} from '@tanstack/react-table'
import {
  DropdownMenu,
  DropdownMenuCheckboxItem,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import { useRouter, useSearchParams } from 'next/navigation'
import { Badge } from '@/components/ui/badge'
import { useState } from 'react'
import { format } from 'date-fns'
import { Checkbox } from '@/components/ui/checkbox'
import { Button } from '@/components/ui/button'
import { Transaction } from '@/types/transaction'
import { ArrowDown, ArrowUpDown, MoreHorizontal } from 'lucide-react'
import { Category } from '@/types/categories'

export const columns: ColumnDef<Transaction>[] = [
  {
    id: 'select',
    header: ({ table }) => {
      return (
        <Checkbox
          checked={
            table.getIsAllPageRowsSelected() ||
            (table.getIsSomePageRowsSelected() && 'indeterminate')
          }
          onCheckedChange={value => table.toggleAllPageRowsSelected(!!value)}
          aria-label="Select all"
        />
      )
    },
    cell: ({ row }) => {
      return (
        <Checkbox
          checked={row.getIsSelected()}
          onCheckedChange={value => row.toggleSelected(!!value)}
          aria-label="Select row"
        />
      )
    },
    enableSorting: false,
    enableHiding: false
  },
  {
    accessorKey: 'amount',
    header: () => <div className="text-left">Amount</div>,
    cell: ({ row }) => {
      const amount = parseFloat(row.getValue('amount'))
      const formatted = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
      }).format(amount)

      return <div className="text-left font-medium">{formatted}</div>
    }
  },
  {
    accessorKey: 'type',
    header: 'Type',
    cell: ({ row }) => {
      const type: string = row.getValue('type')
      const transactionType = type[0].toUpperCase() + type.slice(1)

      if (transactionType === 'Income') return <Badge className="bg-blue-500">Income</Badge>
      return <Badge className="bg-slate-400">{transactionType}</Badge>
    }
  },
  {
    accessorKey: 'paymentMethod',
    header: 'Payment Method',
    cell: ({ row }) => {
      const method: string = row.getValue('paymentMethod')
      return method[0].toUpperCase() + method.slice(1)
    }
  },
  {
    accessorKey: 'status',
    header: 'Status',
    cell: ({ row }) => {
      const status: string = row.getValue('status')
      const transactionStatus = status[0].toUpperCase() + status.slice(1)
      if (transactionStatus === 'Completed') {
        return <Badge className="cursor-pointer bg-green-600 hover:bg-green-700">Completed</Badge>
      } else if (transactionStatus === 'Failed') {
        return (
          <Badge className="" variant={'destructive'}>
            Failed
          </Badge>
        )
      } else {
        return <Badge className="bg-yellow-500">{transactionStatus}</Badge>
      }
    }
  },
  {
    accessorKey: 'date',
    header: ({ column }) => {
      return (
        <Button
          variant={'ghost'}
          className="p-0"
          onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}
        >
          <span className="inline-block font-semibold">Date</span>
          <ArrowUpDown className=" ml-2 h-4 w-4" />
        </Button>
      )
    },
    cell: ({ row }) => {
      const date = row.getValue('date') as string
      const formattedDate = format(date, 'EEEEEEEE, MMMM d, yyyy')

      return <div className="text-left">{formattedDate}</div>
    }
  },
  {
    accessorKey: 'quantity',
    header: 'Quantity',
    cell: ({ row }) => {
      const quantity: number | undefined = row.getValue('quantity')
      return <div className="pl-3">{quantity ?? 'NULL'}</div>
    }
  },
  {
    accessorKey: 'category',
    header: 'Category',
    cell: ({ row }) => {
      const category = row.getValue('category') as Category
      return category?.name ?? 'Uncategorized'
    }
  },
  {
    id: 'actions',
    accessorKey: 'Actions',
    cell: ({ row }) => {
      const transaction = row.original

      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant={'ghost'} className="h-8 w-8 p-0">
              <span className="sr-only">Open menu</span>
              <MoreHorizontal className="h-4 w-4" />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            <DropdownMenuItem
              onClick={() => navigator.clipboard.writeText(transaction.id.toString())}
            >
              Copy transaction ID
            </DropdownMenuItem>
            <DropdownMenuSeparator />
            <DropdownMenuItem>View customer</DropdownMenuItem>
            <DropdownMenuItem>View payment details</DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      )
    }
  }
]

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[]
  data: TData[]
  nextPage?: number | null
  previousPage?: number | null
}

export function TransactionDataTable<TData, TValue>({
  columns,
  data,
  nextPage,
  previousPage
}: DataTableProps<TData, TValue>) {
  const router = useRouter()
  const searchParams = useSearchParams()

  const [sorting, setSorting] = useState<SortingState>([])
  const [columnFilters, setColumnFilters] = useState<ColumnFiltersState>([])
  const [columnVisibility, setColumnVisibility] = useState<VisibilityState>({})

  const goToNextPage = () => {
    const values = Object.fromEntries(searchParams.entries())

    const newSearchParams = new URLSearchParams()
    for (let [key, value] of Object.entries(values)) {
      if (key === 'currentPage') continue
      newSearchParams.append(key, value)
    }

    if (nextPage !== null && nextPage !== undefined) {
      newSearchParams.append('currentPage', nextPage.toString())
    }
    return router.push(`/dashboard/transactions?${newSearchParams.toString()}`)
  }

  const goToPreviousPage = () => {
    const values = Object.fromEntries(searchParams.entries())

    const newSearchParams = new URLSearchParams()
    for (let [key, value] of Object.entries(values)) {
      if (key == 'currentPage') continue
      newSearchParams.append(key, value)
    }

    if (previousPage !== null && previousPage !== undefined) {
      newSearchParams.append('currentPage', previousPage.toString())
    }

    return router.push(`/dashboard/transactions?${newSearchParams.toString()}`)
  }

  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    onSortingChange: setSorting,
    getSortedRowModel: getSortedRowModel(),
    onColumnVisibilityChange: setColumnVisibility,
    onColumnFiltersChange: setColumnFilters,
    getFilteredRowModel: getFilteredRowModel(),
    state: { sorting, columnVisibility, columnFilters: columnFilters }
  })

  return (
    <div>
      <div className="rounded-md border">
        <Table>
          <TableHeader>
            {table.getHeaderGroups().map(headerGroup => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map(header => {
                  return (
                    <TableHead className="font-semibold" key={header.id}>
                      {header.isPlaceholder
                        ? null
                        : flexRender(header.column.columnDef.header, header.getContext())}
                    </TableHead>
                  )
                })}
              </TableRow>
            ))}
          </TableHeader>
          <TableBody>
            {table.getRowModel().rows?.length ? (
              table.getRowModel().rows.map(row => (
                <TableRow key={row.id} data-state={row.getIsSelected() && 'selected'}>
                  {row.getVisibleCells().map(cell => (
                    <TableCell key={cell.id}>
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </TableCell>
                  ))}
                </TableRow>
              ))
            ) : (
              <TableRow>
                <TableCell colSpan={columns.length} className="h-24 text-center">
                  No results.
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </div>
      <div className="flex items-center justify-end space-x-2 py-4">
        <Button
          variant="outline"
          size="sm"
          onClick={goToPreviousPage}
          disabled={previousPage === null || previousPage === undefined}
        >
          Previous
        </Button>
        <Button
          variant="outline"
          size="sm"
          onClick={goToNextPage}
          disabled={nextPage === null || nextPage === undefined}
        >
          Next
        </Button>
      </div>
    </div>
  )
}

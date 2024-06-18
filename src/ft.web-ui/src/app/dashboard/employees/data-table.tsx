'use client'


import * as React from 'react'
import { useRouter, useSearchParams } from 'next/navigation'
import {
  ColumnDef,
  flexRender,
  getCoreRowModel,
  getSortedRowModel,
  SortingState,
  useReactTable
} from '@tanstack/react-table'

import { Button } from '@/components/ui/button'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'

interface DataTableProps<TData, TValue> {
  columns: ColumnDef<TData, TValue>[]
  data: TData[]
  nextPage?: number
  previousPage?: number
}

export function DataTable<TData, TValue>({ columns, data, nextPage, previousPage }: DataTableProps<TData, TValue>) {
  const router = useRouter()
  const searchParams = useSearchParams()
  const [sorting, setSorting] = React.useState<SortingState>([])
  
  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    onSortingChange: setSorting,
    getSortedRowModel: getSortedRowModel(),
    state: {
      sorting
    }
  })

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
    return router.push(`/dashboard/employees?${newSearchParams.toString()}`)
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

    return router.push(`/dashboard/employees?${newSearchParams.toString()}`)
  }

  return (
    <div>
      <div className="rounded-md border">
        <Table>
          <TableHeader className="bg-gray-200">
            {table.getHeaderGroups().map(headerGroup => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map(header => {
                  return (
                    <TableHead key={header.id} className="text-zinc-800 font-bold font-poppins">
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
                    <TableCell key={cell.id} className="font-poppins text-[15px]">
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
        <Button onClick={goToPreviousPage} disabled={previousPage === null || previousPage === undefined}
                variant={'outline'}>Previous</Button>
        <Button onClick={goToNextPage} disabled={nextPage === null || nextPage === undefined}
                variant={'outline'}>Next</Button>
      </div>
    </div>
  )
}

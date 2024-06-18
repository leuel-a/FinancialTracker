'use client'

import { ColumnDef } from '@tanstack/react-table'
import { Employee } from '@/types/employee'
import { Badge } from '@/components/ui/badge'
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from '@/components/ui/dropdown-menu'
import { ArrowUpDown, MoreHorizontal } from 'lucide-react'
import { Button } from '@/components/ui/button'

export const columns: ColumnDef<Employee>[] = [
  {
    accessorKey: 'firstName',
    header: 'First Name'
  },
  {
    accessorKey: 'lastName',
    header: 'Last Name'
  },
  {
    accessorKey: 'email',
    header: 'Email'
  },
  {
    accessorKey: 'salary',
    header: 'Salary',
    cell: ({ row }) => {
      const salary: number = row.getValue('salary')
      const formatted = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD'
      }).format(salary)

      return <div className="text-left font-medium">{formatted}</div>
    }
  },
  {
    accessorKey: 'type',
    header: 'Type',
    cell: ({ row }) => {
      const employeeType = row.getValue('type')
      if (employeeType === 'PartTime')
        return <Badge className="bg-cyan-600">Part-Time</Badge>
      if (employeeType === 'FullTime')
        return <Badge className="bg-orange-700">Full-Time</Badge>
      if (employeeType === 'Contractual')
        return <Badge>Contractual</Badge>
      return <Badge className="bg-emerald-700">Intern</Badge>
    }
  },
  {
    accessorKey: 'hireDate',
    header: ({ column }) => {
      return <Button variant="ghost" onClick={() => column.toggleSorting(column.getIsSorted() === 'asc')}>Hire
        Date <ArrowUpDown className="ml-2 h-4 w-4" /></Button>
    },
    cell: ({ row }) => {
      const hireDate: string = row.getValue('hireDate')

      // convert the string into a date object
      const dateObj = new Date(hireDate)

      // get the year month and day for formatting the output date
      const year = dateObj.getFullYear()
      const month = (dateObj.getMonth() + 1).toString().padStart(2, '0') // Months are zero-indexed, so add 1
      const day = dateObj.getDate().toString().padStart(2, '0')

      return `${year}/${month}/${day}`
    }
  },
  {
    accessorKey: 'bonus',
    header: 'Bonus %'
  },
  {
    accessorKey: 'gender',
    header: 'Gender'
  },
  {
    id: 'actions',
    accessorKey: 'Actions',
    cell: ({ row }) => {
      const employee = row
      return <DropdownMenu>
        <DropdownMenuTrigger>
          <Button variant={'ghost'} className="h-8 w-8 p-0">
            <span className="sr-only">Open menu</span>
            <MoreHorizontal className="h-4 w-4" />
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent align="end">
          <DropdownMenuItem
            onClick={() => navigator.clipboard.writeText(employee.id.toString())}
          >
            Copy Employee ID
          </DropdownMenuItem>
        </DropdownMenuContent>
      </DropdownMenu>
    } 
  }
]

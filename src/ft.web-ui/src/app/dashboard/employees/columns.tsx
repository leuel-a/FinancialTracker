'use client'

import { ColumnDef } from '@tanstack/react-table'
import { Employee } from '@/types/employee'

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
    header: 'Salary'
  },
  {
    accessorKey: 'type',
    header: 'Type'
  }
]

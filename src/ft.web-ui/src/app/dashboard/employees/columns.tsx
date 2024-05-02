'use client'

import { ColumnDef } from '@tanstack/react-table'

export type Employee = {
  id: string
  firstName: string
  lastName: string
  salary: number
  type: string
  email: string
}

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

'use client'

import React from 'react'
import { Role } from '@/types/role'
import { RoleTable } from './data-table'
import { MoreHorizontal } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { ColumnDef } from '@tanstack/react-table'
import LoadingSpinner from '@/components/LoadingSpinner'
import { useGetRolesQuery } from '@/features/role/roleSlice'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import CreateNewRoleDialog from '../components/CreateNewRoleDialog'

const columns: ColumnDef<Role>[] = [
  {
    accessorKey: 'name',
    header: 'Name'
  },
  {
    accessorKey: 'description',
    header: 'Description'
  },
  {
    id: 'actions',
    header: 'Actions',
    cell: ({ row }) => {
      const role = row.original
      return (
        <DropdownMenu>
          <DropdownMenuTrigger asChild>
            <Button variant="ghost" className="h-8 w-8 p-0">
              <span className="sr-only">Open menu</span>
              <MoreHorizontal className="h-4 w-4" />
            </Button>
          </DropdownMenuTrigger>
          <DropdownMenuContent align="end">
            <DropdownMenuItem onClick={() => navigator.clipboard.writeText(role.id.toString())}>
              Copy Role ID
            </DropdownMenuItem>
          </DropdownMenuContent>
        </DropdownMenu>
      )
    }
  }
]

export default function RolesPage() {
  const { data, isLoading, isError, error } = useGetRolesQuery()

  if (isError) {
    console.log(error)
  }
  return (
    <div className="flex flex-col gap-4 pl-5">
      <div className="flex justify-between">
        <h2 className="text-lg font-semibold">Role Management</h2>
        <Button className="w-40 bg-zinc-700 font-semibold">
          <CreateNewRoleDialog />
        </Button>
      </div>
      {isLoading ? (
        <div>
          <LoadingSpinner className="h-10 w-10 mx-auto my-20" />
        </div>
      ) : isError ? (
        <div>Error</div>
      ) : (
        <RoleTable columns={columns} data={data || []} />
      )}
    </div>
  )
}

'use client'

import React, { useEffect } from 'react'
import { Role } from '@/types/role'
import { RoleTable } from './data-table'
import { AlertCircle } from 'lucide-react'
import { MoreHorizontal } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { useSearchParams } from 'next/navigation'
import { ColumnDef } from '@tanstack/react-table'
import LoadingSpinner from '@/components/LoadingSpinner'
import DeleteRoleForm from '../components/DeleteRoleForm'
import { useGetRolesQuery } from '@/features/role/roleSlice'
import CreateNewRoleDialog from '../components/CreateNewRoleDialog'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger
} from '@/components/ui/dropdown-menu'
import { Alert, AlertTitle, AlertDescription } from '@/components/ui/alert'

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
            <DropdownMenuSeparator />
          </DropdownMenuContent>
        </DropdownMenu>
      )
    }
  },
  {
    id: 'deleteRole',
    cell: ({ row }) => {
      const role = row.original
      return <DeleteRoleForm role={role} />
    }
  }
]

export default function RolesPage() {
  const { data, isLoading, isError, error } = useGetRolesQuery()
  const [searchParamsError, setSearchParamsError] = React.useState<string | null>()
  const searchParams = useSearchParams()

  useEffect(() => {
    const error = searchParams.get('error')
    setSearchParamsError(error)
  })

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
          <LoadingSpinner className="mx-auto my-20 h-10 w-10" />
        </div>
      ) : (
        <RoleTable columns={columns} data={data || []} />
      )}

      {searchParamsError && (
        <Alert variant="destructive">
          <AlertCircle className="h-4 w-4" />
          <AlertTitle>Error</AlertTitle>
          <AlertDescription>
            {searchParamsError ? searchParamsError : 'Something went wrong please try again later.'}
          </AlertDescription>
        </Alert>
      )}
    </div>
  )
}

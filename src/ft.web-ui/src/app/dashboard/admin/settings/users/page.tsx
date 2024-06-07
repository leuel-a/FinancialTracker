'use client'

import React from 'react'
import Link from 'next/link'

import { User } from '@/types/user'
import { useGetAllUsersQuery } from '@/features/user/userSlice'
import { columns, UserDataTable } from '../components/UsersTable'

import { Terminal } from 'lucide-react'
import { Button } from '@/components/ui/button'
import LoadingSpinner from '@/components/LoadingSpinner'
import { Alert, AlertDescription, AlertTitle } from '@/components/ui/alert'

export default function UserSettingsPage({}) {
  const { data, isLoading, isError, error } = useGetAllUsersQuery()
  return (
    <div className="space-y-4 pl-5">
      <div className="flex justify-between">
        <h2 className="text-lg font-semibold">User Management</h2>
        <Link href="">
          <Button className="w-44 bg-zinc-800">Add User</Button>
        </Link>
      </div>
      {isLoading ? (
        <div className="mt-20 flex justify-center">
          <LoadingSpinner />
        </div>
      ) : isError ? (
        <Alert variant="destructive">
          <Terminal className="h-4 w-4" />
          <AlertTitle>Heads up! {'status' in error ? `Status: ${error.status}` : ''}</AlertTitle>
          <AlertDescription>Something went wrong</AlertDescription>
        </Alert>
      ) : (
        <UserDataTable columns={columns} data={data as User[]} />
      )}
    </div>
  )
}

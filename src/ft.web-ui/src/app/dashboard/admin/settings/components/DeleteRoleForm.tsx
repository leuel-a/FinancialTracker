'use client'
import React from 'react'
import { Role } from '@/types/role'
import { useRouter } from 'next/navigation'
import { Button } from '@/components/ui/button'
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
  AlertDialogTrigger
} from '@/components/ui/alert-dialog'
import { useDeleteRoleMutation } from '@/features/role/roleSlice'

interface DeleteRoleFormProps {
  role: Role
}

export default function DeleteRoleForm({ role: { id, name } }: DeleteRoleFormProps) {
  const router = useRouter()
  const [deleteRole, { isLoading, isError, error }] = useDeleteRoleMutation()

  const handleSubmit: React.MouseEventHandler<HTMLButtonElement> = async e => {
    try {
      await deleteRole(id).unwrap()
      router.replace('/dashboard/admin/settings/roles')
    } catch (e: any) {
      const searchParams = new URLSearchParams()

      // Set the error as a query parameter and redirect back to the roles page
      searchParams.set('error', e.message)
      router.push(`/dashboard/admin/settings/roles?${searchParams.toString()}`)
    }
  }

  return (
    <form>
      <div className="flex justify-center gap-4">
        <AlertDialog>
          <AlertDialogTrigger asChild>
            <Button variant="destructive">Delete</Button>
          </AlertDialogTrigger>
          <AlertDialogContent>
            <AlertDialogHeader>
              <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
              <AlertDialogDescription>
                This action cannot be undone. This will permanently delete the role{' '}
                <span className="font-semibold underline">{name}</span> and remove your data from
                our servers.
              </AlertDialogDescription>
            </AlertDialogHeader>
            <AlertDialogFooter>
              <AlertDialogCancel>Cancel</AlertDialogCancel>
              <AlertDialogAction onClick={handleSubmit}>Continue</AlertDialogAction>
            </AlertDialogFooter>
          </AlertDialogContent>
        </AlertDialog>
      </div>
    </form>
  )
}

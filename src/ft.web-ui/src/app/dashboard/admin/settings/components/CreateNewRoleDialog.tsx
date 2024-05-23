'use client'

import { z } from 'zod'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import {
  Form,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
  FormControl
} from '@/components/ui/form'
import { Role } from '@/types/role'
import { Button } from '@/components/ui/button'
import { Textarea } from '@/components/ui/textarea'
import { useCreateRoleMutation } from '@/features/role/roleSlice'
import LoadingSpinner from '@/components/LoadingSpinner'
import { useRouter } from 'next/navigation'
import { revalidatePath } from 'next/cache'

const createRoleSchema = z.object({
  name: z.string({ required_error: 'Name is required' }),
  description: z.string({ required_error: 'Description is required' })
})

export default function CreateNewRoleDialog() {
  const form = useForm<z.infer<typeof createRoleSchema>>({
    resolver: zodResolver(createRoleSchema)
  })
  const router = useRouter()

  const [createRole, { isLoading, isError }] = useCreateRoleMutation()

  function onSubmit(values: z.infer<typeof createRoleSchema>) {
    // create a role to send to the server
    const newRole: Partial<Role> = values
    createRole(newRole)
  }

  return (
    <Dialog>
      <DialogTrigger>Create Role</DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Create Role</DialogTitle>
          <DialogDescription>Please enter the details below to create a new role</DialogDescription>
        </DialogHeader>
        <Form {...form}>
          <form onSubmit={form.handleSubmit(onSubmit)} className="flex flex-col gap-6">
            <FormField
              control={form.control}
              name="name"
              render={({ field }) => {
                return (
                  <FormItem>
                    <FormLabel className="font-semibold">Name</FormLabel>
                    <FormControl>
                      <Input placeholder="Admin" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )
              }}
            />
            <FormField
              control={form.control}
              name="description"
              render={({ field }) => {
                return (
                  <FormItem>
                    <FormLabel className="font-semibold">Description</FormLabel>
                    <FormControl>
                      <Textarea rows={15} placeholder="Write the description here..." {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )
              }}
            />
            <Button disabled={isLoading} className="bg-zinc-700 font-semibold">
              {isLoading ? <LoadingSpinner /> : 'Create Role'}
            </Button>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  )
}

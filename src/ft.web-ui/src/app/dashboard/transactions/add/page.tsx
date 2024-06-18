'use client'

import Link from 'next/link'
import { Undo2 } from 'lucide-react'
import PageTitle from '../../components/PageTitle'
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage
} from '@/components/ui/form'
import { z } from 'zod'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import React, { ReactNode } from 'react'
import { Input } from '@/components/ui/input'
import { Textarea } from '@/components/ui/textarea'
import { Button } from '@/components/ui/button'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import { useGetAllCategoriesQuery } from '@/features/categories/categoriesApi'
import { Category } from '@/types/categories'

const newTransactionSchema = z.object({
  status: z.string({ required_error: 'Status is required' }),
  amount: z
    .number({ required_error: 'Amount is required', message: 'Amount must be a number' })
    .gt(1, 'Amount must be greater than 1.'),
  type: z.string({ required_error: 'Type is required' }),
  date: z.date({ required_error: 'Date is required' }),
  accountNumber: z.string().optional(),
  description: z.string({ required_error: 'Description is required' }),
  numOfItems: z.number().optional(),
  costPerItem: z.number().optional(),
  paymentMethod: z.string().optional(),
  region: z.string().optional(),
  categoryId: z.number().optional(),
  quantity: z.number().optional()
})

export default function AddTransaction() {
  const form = useForm<z.infer<typeof newTransactionSchema>>({
    resolver: zodResolver(newTransactionSchema)
  })
  const [categories, setCategories] = React.useState<Category[]>([])
  const { data, isLoading, isError } = useGetAllCategoriesQuery('')

  React.useEffect(() => {
    if (isLoading == false && data) {
      setCategories(data.data)
    }
  }, [isLoading])

  function onSubmit(values: z.infer<typeof newTransactionSchema>) {
    console.log(values)
  }

  return (
    <div>
      <div className="flex justify-between">
        <PageTitle title="Add Transaction" />
        <Link
          href="/dashboard/transactions"
          className="flex cursor-pointer items-center gap-2 rounded-lg border border-black px-4 py-2 hover:border hover:shadow-md"
        >
          <Undo2 />
          <p>Back to Transactions</p>
        </Link>
      </div>
      {isLoading ? (
        <div></div>
      ) : (
        <div className="mt-4 space-y-6">
          <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
              <FormField
                control={form.control}
                name="description"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Description</FormLabel>
                      <FormControl>
                        <Textarea
                          placeholder="Enter the description for the transaction..."
                          {...field}
                          rows={10}
                        />
                      </FormControl>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
              <div className="grid grid-cols-2 gap-4">
                <FormField
                  control={form.control}
                  name="amount"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Amount</FormLabel>
                        <FormControl>
                          <Input type={'number'} placeholder="123,455.45" />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
                <FormField
                  control={form.control}
                  name="date"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Date</FormLabel>
                        <FormControl>
                          <Input type={'date'} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
              </div>
              <FormField
                control={form.control}
                name="status"
                render={({ field }) => {
                  return (
                    <FormItem>
                      <FormLabel className="font-semibold">Status</FormLabel>
                      <Select onValueChange={field.onChange} defaultValue={field.value}>
                        <FormControl>
                          <SelectTrigger>
                            <SelectValue
                              className="text-gray-500"
                              placeholder="Select status for the transaction..."
                            />
                          </SelectTrigger>
                        </FormControl>
                        <SelectContent>
                          <SelectItem value="Failed">Failed</SelectItem>
                          <SelectItem value="Pending">Pending</SelectItem>
                          <SelectItem value="Completed">Completed</SelectItem>
                          <SelectItem value="Active">Active</SelectItem>
                          <SelectItem value="Paused">Paused</SelectItem>
                        </SelectContent>
                      </Select>
                      <FormMessage />
                    </FormItem>
                  )
                }}
              />
              <div className="grid grid-cols-2 gap-4">
                <FormField
                  control={form.control}
                  name="type"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Type</FormLabel>
                        <Select onValueChange={field.onChange} defaultValue={field.value}>
                          <FormControl>
                            <SelectTrigger>
                              <SelectValue
                                className="text-gray-500"
                                placeholder="Select type for the transaction..."
                              />
                            </SelectTrigger>
                          </FormControl>
                          <SelectContent>
                            <SelectItem value="Income">Income</SelectItem>
                            <SelectItem value="Expense">Expense</SelectItem>
                          </SelectContent>
                        </Select>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
                <FormField
                  control={form.control}
                  name="categoryId"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormItem className="font-semibold">Category</FormItem>
                        <Select>
                          <FormControl>
                            <SelectTrigger>
                              <SelectValue
                                placeholder="Please enter a category..."
                                className="text-gray-500"
                              />
                            </SelectTrigger>
                          </FormControl>
                          <SelectContent>
                            {categories.map(category => (
                              <SelectItem key={category.id} value={category.id}>
                                {category.name}
                              </SelectItem>
                            ))}
                          </SelectContent>
                        </Select>
                      </FormItem>
                    )
                  }}
                />
              </div>
              <div className="grid grid-cols-2 gap-4">
                <FormField
                  control={form.control}
                  name="paymentMethod"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Payment Method</FormLabel>
                        <FormControl>
                          <Input placeholder="Cash, Bank Transfer..." {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
                <FormField
                  control={form.control}
                  name="accountNumber"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Account Number</FormLabel>
                        <FormControl>
                          <Input placeholder="10000023673..." {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
              </div>
              <div className="grid grid-cols-3 gap-2">
                <FormField
                  control={form.control}
                  name="numOfItems"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Number of Items</FormLabel>
                        <FormControl>
                          <Input type="number" placeholder="10" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
                <FormField
                  control={form.control}
                  name="costPerItem"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Cost Per Item</FormLabel>
                        <FormControl>
                          <Input type="number" placeholder="1000" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
                <FormField
                  control={form.control}
                  name="quantity"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormLabel className="font-semibold">Quantity</FormLabel>
                        <FormControl>
                          <Input type="number" placeholder="10" {...field} />
                        </FormControl>
                        <FormMessage />
                      </FormItem>
                    )
                  }}
                />
              </div>
              <Button type={'submit'} className="w-full bg-zinc-800">
                Submit
              </Button>
            </form>
          </Form>
        </div>
      )}
    </div>
  )
}

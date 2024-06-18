'use client'

import { z } from 'zod'
import React from 'react'
import Link from 'next/link'
import { format } from 'date-fns'
import { Undo2 } from 'lucide-react'
import { useForm } from 'react-hook-form'
import { useRouter } from 'next/navigation'
import { Category } from '@/types/categories'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'
import { toast } from '@/components/ui/use-toast'
import { Transaction } from '@/types/transaction'
import PageTitle from '../../components/PageTitle'
import { Textarea } from '@/components/ui/textarea'
import { zodResolver } from '@hookform/resolvers/zod'
import LoadingSpinner from '@/components/LoadingSpinner'
import { useGetAllCategoriesQuery } from '@/features/categories/categoriesApi'
import { useCreateTransactionMutation } from '@/features/transaction/transactionSlice'
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from '@/components/ui/select'
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from '@/components/ui/form'

const newTransactionSchema = z.object({
  status: z.string({ required_error: 'Status is required' }),
  amount: z
    .string({ required_error: 'Amount is required' }),
  type: z.string({ required_error: 'Type is required' }),
  date: z.string({ required_error: 'Date is required' }),
  accountNumber: z.string().optional(),
  description: z.string({ required_error: 'Description is required' }),
  numOfItems: z.number().optional(),
  costPerItem: z.number().optional(),
  paymentMethod: z.string().optional(),
  region: z.string().optional(),
  category: z.string({ required_error: 'Category is required.' }),
  quantity: z.number().optional()
})

export default function AddTransaction() {
  const form = useForm<z.infer<typeof newTransactionSchema>>({
    resolver: zodResolver(newTransactionSchema)
  })

  const router = useRouter()
  const [categories, setCategories] = React.useState<Category[]>([])
  const [createTransaction, results] = useCreateTransactionMutation()
  const [selectedCategory, setSelectedCategory] = React.useState<number>()
  const { data, isLoading, isError } = useGetAllCategoriesQuery('')

  React.useEffect(() => {
    if (isLoading == false && data) {
      setCategories(data.data)
    }
  }, [isLoading])

  async function onSubmit(values: z.infer<typeof newTransactionSchema>) {
    const { date, amount, category, ...rest } = values

    const transaction: Partial<Transaction> = {
      ...rest,
      date: format(new Date(date), 'yyyy/MM/dd'),
      amount: parseFloat(amount),
      categoryId: selectedCategory
    }

    const result = await createTransaction(transaction)

    if ('error' in result) {
      console.log(result.error)
    } else {
      const { data } = result
      toast({
        title: 'Transaction created successfully.',
        description: `Transaction with amount ${data.amount}`
      })
      return router.push('/dashboard/transactions')
    }
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
        <div className="h-full w-full">
          <LoadingSpinner className="h-4 w-4" />
        </div>
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
                          <Input type={'number'} placeholder="123,455.45" {...field} />
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
                          <Input type={'date'} placeholder="Enter the date of transaction" {...field} />
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
                  name="category"
                  render={({ field }) => {
                    return (
                      <FormItem>
                        <FormItem className="font-semibold">Category</FormItem>
                        <Select onValueChange={field.onChange} defaultValue={field.value}>
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
                              <SelectItem onClick={() => {
                                setSelectedCategory(category.id)
                              }} key={category.id}
                                          value={category.name}>
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
              <Button disabled={isLoading} type={'submit'} className="w-full bg-zinc-800">
                {isLoading ? <LoadingSpinner /> : 'Submit'}
              </Button>
            </form>
          </Form>
        </div>
      )}
    </div>
  )
}

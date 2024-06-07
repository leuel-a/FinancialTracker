'use client'

import * as React from 'react'
import { Slider } from '@/components/ui/slider'
import { SlidersHorizontal } from 'lucide-react'

import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue
} from '@/components/ui/select'
import { Button } from '@/components/ui/button'
import { Label } from '@/components/ui/label'
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'
import { useGetAllCategoriesQuery } from '@/features/categories/categoriesApi'
import { useRouter, useSearchParams } from 'next/navigation'

export default function TransactionsFilter() {
  const [minAmount, setMinAmount] = React.useState<number[]>([0])
  const [maxAmount, setMaxAmount] = React.useState<number[]>([10000])
  const [status, setStatus] = React.useState<string>('')
  const [type, setType] = React.useState<string>('')
  const [category, setCategory] = React.useState<string>('')

  const router = useRouter()
  const searchParams = useSearchParams()

  const handleFilters = () => {
    const filters = {
      minAmount: minAmount[0].toString(),
      maxAmount: maxAmount[0].toString(),
      status,
      type,
      category
    }

    const values = Object.fromEntries(searchParams.entries())
    const newSearchParams = new URLSearchParams()
    for (let [key, value] of Object.entries(values)) {
      if (key === 'StartDate' || key === 'EndDate') {
        newSearchParams.append(key, value)
      }
    }

    for (let [key, value] of Object.entries(filters)) {
      newSearchParams.append(key, value)
    }
    return router.push(`/dashboard/transactions?${newSearchParams.toString()}`)
  }

  const { isLoading, isError, data } = useGetAllCategoriesQuery({})

  const formattedMin = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(minAmount[0])

  const formattedMax = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
  }).format(maxAmount[0])

  return (
    <Popover>
      <PopoverTrigger>
        <SlidersHorizontal className="cursor-pointer" />
      </PopoverTrigger>
      <PopoverContent className="w-[500px]">
        <div className="space-y-5">
          <div>
            <h3 className="text-lg font-bold">Apply Filters</h3>
            <p className="text-sm text-gray-500">Choose from the filters below to apply filters</p>
          </div>
          <div className="grid grid-cols-4 items-center gap-0.5">
            <Label className="font-semibold">Min Amount</Label>
            <div className="col-span-3 grid grid-cols-4 gap-4">
              <Slider
                className="col-span-3"
                defaultValue={[0]}
                max={10000}
                onValueChange={setMinAmount}
                value={minAmount}
              />
              <p>{formattedMin}</p>
            </div>
          </div>
          <div className="grid grid-cols-4 items-center gap-0.5">
            <Label className="font-semibold">Max Amount</Label>
            <div className="col-span-3 grid grid-cols-4 gap-4">
              <Slider
                className="col-span-3"
                defaultValue={[0]}
                max={10000}
                onValueChange={setMaxAmount}
                value={maxAmount}
              />
              <p>{formattedMax}</p>
            </div>
          </div>
          <div className="grid grid-cols-4 items-center gap-0.5">
            <Label className="font-semibold">Categories</Label>
            <Select value={category} onValueChange={setCategory}>
              <SelectTrigger className="col-span-3">
                <SelectValue placeholder="Select a category" />
              </SelectTrigger>
              <SelectContent>
                {isLoading !== true && isError !== true ? (
                  data?.data.map(category => {
                    return <SelectItem value={category.name}>{category.name}</SelectItem>
                  })
                ) : (
                  <div>Something went wrong</div>
                )}
              </SelectContent>
            </Select>
          </div>
          <div className="grid grid-cols-4">
            <Label className="font-semibold">Type</Label>
            <Select value={type} onValueChange={setType}>
              <SelectTrigger className="col-span-3">
                <SelectValue placeholder="Select a transaction type" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Income">Income</SelectItem>
                <SelectItem value="Expense">Expense</SelectItem>
              </SelectContent>
            </Select>
          </div>
          <div className="grid grid-cols-4">
            <Label className="font-semibold">Status</Label>
            <Select value={status} onValueChange={setStatus}>
              <SelectTrigger className="col-span-3">
                <SelectValue placeholder="Select status of a transaction" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="Completed">Completed</SelectItem>
                <SelectItem value="Failed">Failed</SelectItem>
                <SelectItem value="Pending">Pending</SelectItem>
              </SelectContent>
            </Select>
          </div>
          <Button onClick={handleFilters} className="w-full bg-zinc-800">
            Apply
          </Button>
        </div>
      </PopoverContent>
    </Popover>
  )
}

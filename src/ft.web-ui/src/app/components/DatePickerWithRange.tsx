'use client'

import * as React from 'react'
import { useSearchParams } from 'next/navigation'
import { addDays, format } from 'date-fns'
import { Calendar as CalendarIcon } from 'lucide-react'
import { useRouter } from 'next/navigation'
import { DateRange } from 'react-day-picker'

import { cn } from '@/lib/utils'
import { Button } from '@/components/ui/button'
import { Calendar } from '@/components/ui/calendar'
import { Popover, PopoverContent, PopoverTrigger } from '@/components/ui/popover'

export default function DatePickerWithRange({ className }: React.HTMLAttributes<HTMLDivElement>) {
  const router = useRouter()
  const searchParams = useSearchParams()
  const [date, setDate] = React.useState<DateRange | undefined>({
    from: undefined,
    to: undefined
  })

  const handleClearDate = () => {
    setDate({ from: undefined, to: undefined })
    const values = Object.fromEntries(searchParams.entries())

    const newSearchParams = new URLSearchParams()
    for (let [key, value] of Object.entries(values)) {
      if (key === 'StartDate' || key === 'EndDate') continue
      newSearchParams.append(key, value)
    }
    return router.push(`/dashboard/transactions?${newSearchParams.toString()}`)
  }

  const handleApplyDate = () => {
    if (date !== undefined) {
      const { from, to } = date

      const startDate = from ? format(from, 'yyyy/MM/dd') : ''
      const endDate = to ? format(to, 'yyyy/MM/dd') : ''

      const values = Object.fromEntries(searchParams.entries())

      const newSearchParams = new URLSearchParams()
      for (let [key, value] of Object.entries(values)) {
        newSearchParams.append(key, value)
      }

      // There might be a better way to do this
      newSearchParams.append('StartDate', startDate)
      newSearchParams.append('EndDate', endDate)
      return router.push(`/dashboard/transactions?${newSearchParams.toString()}`)
    }
  }

  return (
    <div className={cn('grid gap-2', className)}>
      <Popover>
        <PopoverTrigger asChild>
          <Button
            id="date"
            variant={'outline'}
            className={cn(
              'w-[300px] justify-start text-left font-normal',
              !date && 'text-muted-foreground'
            )}
          >
            <CalendarIcon className="mr-2 h-4 w-4" />
            {date?.from ? (
              date.to ? (
                <>
                  {format(date.from, 'LLL dd, y')} - {format(date.to, 'LLL dd, y')}
                </>
              ) : (
                format(date.from, 'LLL dd, y')
              )
            ) : (
              <span>Pick a date range</span>
            )}
          </Button>
        </PopoverTrigger>
        <PopoverContent className="w-auto p-3" align="start">
          <Calendar
            initialFocus
            mode="range"
            defaultMonth={date?.from}
            selected={date}
            onSelect={setDate}
            numberOfMonths={2}
          />
          <div className="flex gap-2">
            <Button variant={'ghost'} className="flex-1" onClick={handleClearDate}>
              Clear Range
            </Button>
            <Button variant={'outline'} className="ml-auto flex-1" onClick={handleApplyDate}>
              Apply Date Range
            </Button>
          </div>
        </PopoverContent>
      </Popover>
    </div>
  )
}

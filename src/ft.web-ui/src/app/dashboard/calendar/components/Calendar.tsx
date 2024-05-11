'use client'

import React from 'react'
import { getDaysInMonth } from 'date-fns'
import CalendarCell from './CalendarCell'
import { Button } from '@/components/ui/button'
import { ChevronLeft, ChevronRight } from 'lucide-react'
import PageTitle from '../../components/PageTitle'

type Props = {}

function getStartDayOfMonth(year: number, month: number) {
  const date = new Date(year, month - 1, 1)
  return date.getDay()
}

const drawGrid = (month: number, year: number, rows: number = 5) => {
  const startDate = getStartDayOfMonth(year, month)
  const daysInMonth = getDaysInMonth(new Date(year, month + 1))
  const previousMonthDays = getDaysInMonth(new Date(year, month))

  console.log(startDate, 'Start Date')

  let day = 1
  const grid = []
  let foundStartDate = false

  for (let i = 0; i < rows; i++) {
    const row = []

    for (let j = 0; j < 7; j++) {
      if (day > daysInMonth) day = 1

      if (!foundStartDate) {
        if (j === startDate) {
          foundStartDate = true
        } else {
          row.push(<CalendarCell day={previousMonthDays - startDate + j + 1} />)
        }
      }

      if (foundStartDate) {
        row.push(<CalendarCell day={day} />)
        day++
      }
    }
    grid.push(row)
  }

  return grid
}

export default function Calendar({}: Props) {
  const [month, setMonth] = React.useState<number>(new Date().getMonth() + 1)
  const [year, setYear] = React.useState<number>(new Date().getFullYear())

  const handleCalendarNavigationClick = (month: number, year: number) => {
    setMonth(month)
    setYear(year)
  }

  return (
    <div className="flex flex-col gap-5">
      <PageTitle title={`${month} - ${year}`} />
      <div className="flex w-full justify-end gap-2">
        <Button
          onClick={() => {
            // get previous month and year
            const previousMonth = month === 1 ? 12 : month - 1
            const previousYear = month === 1 ? year - 1 : year

            handleCalendarNavigationClick(previousMonth, previousYear)
          }}
          className="border border-black font-semibold shadow-md"
          variant={'outline'}
        >
          <ChevronLeft />
        </Button>
        <Button
          onClick={() => {
            const nextMonth = month === 12 ? 1 : month + 1
            const nextYear = month === 12 ? year + 1 : year

            handleCalendarNavigationClick(nextMonth, nextYear)
          }}
          className="border border-black font-semibold shadow-md"
          variant={'outline'}
        >
          <ChevronRight />
        </Button>
      </div>
      <div className="grid grid-cols-7 gap-2">
        {['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'].map(day => (
          <div className="rounded bg-gray-700 px-2 py-1 text-white">{day}</div>
        ))}
        {drawGrid(month, year)}
      </div>
    </div>
  )
}

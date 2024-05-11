'use client'

import React from 'react'
import PageTitle from '../components/PageTitle'
import { Button } from '@/components/ui/button'
import { ChevronLeft, ChevronRight } from 'lucide-react'
import CalendarCell from './components/CalendarCell'
import Calendar from './components/Calendar'

type CalendarPageProps = {
  searchParams?: { month: string; year: string }
}

export default function CalendarPage({ searchParams }: CalendarPageProps) {
  const month = parseInt(searchParams?.month!) || new Date().getMonth() + 1
  const year = parseInt(searchParams?.year!) || new Date().getFullYear()

  return (
    <div className="flex flex-col gap-7">
      <PageTitle title="Calendar" />
      <Calendar />
    </div>
  )
}

'use client'

import { useGetTransactionBreakdownForMonthQuery } from '@/features/transaction/transactionSlice'
import { Area, AreaChart, CartesianGrid, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts'
import LoadingSpinner from '@/components/LoadingSpinner'
import React from 'react'

interface SingleMonthLineChartProps {
  month: number
  year: number
}


export default function SingleMonthAreaChart({ month, year }: SingleMonthLineChartProps) {
  const { data: summaries, isLoading, isError } = useGetTransactionBreakdownForMonthQuery({ month, year })
  return <ResponsiveContainer>
    {isLoading ? (
        <div className="w-full h-full flex items-center justify-center"><LoadingSpinner /></div>
      ) :
      <AreaChart width={730} height={250} data={summaries?.values.map(summary => ({
        day: summary.day,
        income: summary.income,
        expense: summary.expense
      }))}
                 margin={{ top: 10, right: 30, left: 0, bottom: 0 }}>
        <defs>
          <linearGradient id="colorUv" x1="0" y1="0" x2="0" y2="1">
            <stop offset="5%" stopColor="#8884d8" stopOpacity={0.8} />
            <stop offset="95%" stopColor="#8884d8" stopOpacity={0} />
          </linearGradient>
          <linearGradient id="colorPv" x1="0" y1="0" x2="0" y2="1">
            <stop offset="5%" stopColor="#82ca9d" stopOpacity={0.8} />
            <stop offset="95%" stopColor="#82ca9d" stopOpacity={0} />
          </linearGradient>
        </defs>
        <XAxis fontSize={11} dataKey="day" />
        <YAxis tickFormatter={(value) => `$${value}`} />
        <CartesianGrid strokeDasharray="3 3" />
        <Tooltip />
        <Legend/>
        <Area type="monotone" dataKey="income" stroke="#8884d8" fillOpacity={1} fill="url(#colorUv)" />
        <Area type="monotone" dataKey="expense" stroke="#82ca9d" fillOpacity={1} fill="url(#colorPv)" />
      </AreaChart>
    }

  </ResponsiveContainer>
}
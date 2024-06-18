'use client'

import { useGetTransactionBreakdownForMonthQuery } from '@/features/transaction/transactionSlice'
import { CartesianGrid, Legend, Line, LineChart, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts'
import LoadingSpinner from '@/components/LoadingSpinner'
import { months } from '@/types/transaction'

interface SingleMonthLineChartProps {
  month: number
  year: number
}


export default function SingleMonthLineChart({ month, year }: SingleMonthLineChartProps) {
  const { data: summaries, isLoading, isError } = useGetTransactionBreakdownForMonthQuery({ month, year })
  return <ResponsiveContainer>
    {isLoading ? (
      <div className="w-full h-full flex items-center justify-center"><LoadingSpinner /></div>
    ) :
      <LineChart
        width={500}
        height={300}
        data={summaries?.values.map(summary => ({
          day: summary.day,
          income: summary.income,
          expense: summary.expense,
          totalAmount: summary.totalAmount
        }))}
        margin={{
          top: 5,
          right: 30,
          left: 20,
          bottom: 5
        }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="day" fontSize={12} />
        <YAxis tickFormatter={value => `$${value}`} />
        <Tooltip />
        <Legend />
        <Line type="monotone" dataKey="expense" stroke="#82ca9d" />
        <Line type="monotone" dataKey="income" stroke="#005377" activeDot={{ r: 8 }} />
        <Line type="monotone" dataKey="totalAmount" stroke="#005343"/>
      </LineChart>
    }
    
  </ResponsiveContainer>
}
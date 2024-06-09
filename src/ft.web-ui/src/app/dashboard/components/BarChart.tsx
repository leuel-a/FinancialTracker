'use client'

import React from 'react'
import {months} from '@/types/transaction'
import LoadingSpinner from '@/components/LoadingSpinner'
import { Bar, BarChart as BarGraph, Legend, ResponsiveContainer, Tooltip, XAxis, YAxis } from 'recharts'

import { useGetTransactionSummaryForYearQuery } from '@/features/transaction/transactionSlice'


export default function BarChart({}) {
  const { isLoading, isError, data: transactions } = useGetTransactionSummaryForYearQuery(2023)
  
  return (
    <ResponsiveContainer width={'100%'} height={350}>
      {isLoading ? <div className="w-full h-full flex items-center justify-center"><LoadingSpinner /></div> : (
        <BarGraph data={transactions?.map(transaction => ({
          ...transaction,
          month: months[transaction.month as number].slice(0, 3)
        }))}>
          <XAxis dataKey={'month'} tickLine={false} axisLine={false} stroke="#888888" fontSize={12} />
          <YAxis tickLine={false} axisLine={false} stroke="#888888" fontSize={12}
                 tickFormatter={(value) => `$${value}`} />
          <Tooltip />
          <Legend />
          <Bar dataKey={'expense'} radius={[4, 4, 0, 0]} fill="#005377" />
          <Bar dataKey={'income'} radius={[4, 4, 0, 0]} fill="#06A77D" />
        </BarGraph>)}
    </ResponsiveContainer>
  )
}

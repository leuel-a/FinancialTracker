'use client'

import * as React from 'react'
import Link from 'next/link'
import { useSearchParams } from 'next/navigation'
import TransactionsFilter from './components/TransactionsFilter'
import PageTitle from '../components/PageTitle'
import { Button } from '@/components/ui/button'
import { TransactionDataTable, columns } from './data-table'
import DatePickerWithRange from '@/app/components/DatePickerWithRange'
import { useGetAllTransactionsQuery } from '@/features/transaction/transactionSlice'
import LoadingSpinner from '@/components/LoadingSpinner'

export default function TransactionsPage() {
  const searchParams = useSearchParams()
  const { isLoading: loadingT, data: transactions } = useGetAllTransactionsQuery(
    searchParams.toString()
  )

  return (
    <div className="relative space-y-2">
      <PageTitle title="Transactions" />
      <div className="flex gap-2">
        <div className="flex items-center justify-center gap-3">
          <h3 className="text-sm font-semibold">Date Range</h3>
          <DatePickerWithRange />
          <TransactionsFilter />
        </div>
      </div>
      {loadingT ? (
        <LoadingSpinner />
      ) : (
        <>
          <TransactionDataTable
            nextPage={transactions?.nextPage}
            previousPage={transactions?.previousPage}
            columns={columns}
            data={transactions?.data || []}
          />
          <Link href="/dashboard/transactions/add">
            <Button className="absolute bottom-3 w-52 bg-zinc-700">Add Transaction</Button>
          </Link>
        </>
      )}
    </div>
  )
}

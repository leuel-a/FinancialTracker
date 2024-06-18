'use client'

import Link from 'next/link'
import * as React from 'react'
import { Button } from '@/components/ui/button'
import PageTitle from '../components/PageTitle'
import { useRouter, useSearchParams } from 'next/navigation'
import { columns, TransactionDataTable } from './data-table'
import { TableSkeleton } from '@/app/components/TableSkeleton'
import TransactionsFilter from './components/TransactionsFilter'
import DatePickerWithRange from '@/app/components/DatePickerWithRange'
import { useGetAllTransactionsQuery } from '@/features/transaction/transactionSlice'
import { CircleX } from 'lucide-react'
import { router } from 'next/client'

export default function TransactionsPage() {
  const router = useRouter()
  const searchParams = useSearchParams()
  const [filtersPresent, setFiltersPresent] = React.useState(false)
  const { isLoading: loadingT, data: transactions } = useGetAllTransactionsQuery(
    searchParams.toString()
  )
  
  const clearFilters = () => {
    router.push('/dashboard/transactions')
  }

  React.useEffect(() => {
    if (searchParams.toString().length > 0) {
      setFiltersPresent(prevState => !prevState)
    }
    
    if (searchParams.toString().length === 0) {
      setFiltersPresent(prevState => !prevState)
    }
  }, [searchParams.toString()])

  return (
    <div className="relative space-y-2">
      <PageTitle title="Transactions" />
      <div className="flex gap-2">
        <div className="flex items-center justify-center gap-3">
          <h3 className="text-sm font-semibold">Date Range</h3>
          <DatePickerWithRange />
          <TransactionsFilter />
          {filtersPresent && <CircleX className="cursor-pointer" onClick={clearFilters} />}
        </div>
      </div>
      {loadingT ? (
        <TableSkeleton />
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

import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'

import { MonthlyTransactionSummary, Transaction } from '@/types/transaction'
import { PaginatedResponse } from '@/types/paginated'

type TransactionQueryParameters = {
  currentPage?: number
  pageSize?: number
  startDate?: string
  endDate?: string
  status?: string
  type?: string
}

export const transactionApi = createApi({
  reducerPath: 'transactionApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllTransactions: builder.query<PaginatedResponse<Transaction>, string>({
      query: params => {
        return `api/transactions?${params}`
      },
    }),
    getTransactionSummaryForYear: builder.query<MonthlyTransactionSummary[], number>({
      query: year => `api/transactions/year/${year}`
    })
  })
})

export const { useGetTransactionSummaryForYearQuery, useGetAllTransactionsQuery } = transactionApi

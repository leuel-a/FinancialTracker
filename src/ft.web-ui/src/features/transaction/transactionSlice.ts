import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'

import { MonthlyTransactionSummary, Transaction } from '@/types/transaction'
import { PaginatedResponse } from '@/types/paginated'

type SingleDaySummary = {
  day: string
  income: number
  expense: number
  totalAmount: number
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
    }),
    createTransaction: builder.mutation<Transaction, Partial<Transaction>>({
      query: body => ({
        url: 'api/transactions',
        method: 'POST',
        body
      })
    }),
    getTransactionBreakdownForMonth: builder.query<{ values: SingleDaySummary[] }, { month: number, year: number }>({
      query: ({ month, year }) => `api/transactions/year/${year}/month/${month}/breakdown`
    })
  })
})

export const {
  useGetTransactionSummaryForYearQuery,
  useCreateTransactionMutation,
  useGetAllTransactionsQuery,
  useGetTransactionBreakdownForMonthQuery
} = transactionApi

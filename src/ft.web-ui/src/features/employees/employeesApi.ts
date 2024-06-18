import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'
import { PaginatedResponse } from '@/types/paginated'
import { Employee } from '@/types/employee'

export const employeesApi = createApi({
  reducerPath: 'employeesApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllEmployees: builder.query<PaginatedResponse<Employee>, string>({
      query: params => `api/employees?${params}`
    })
  })
})

export const { useGetAllEmployeesQuery } = employeesApi
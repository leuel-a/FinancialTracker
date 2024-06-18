import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'

import { Category } from '@/types/categories'
import { PaginatedResponse } from '@/types/paginated'

export const categoriesApi = createApi({
  reducerPath: 'categoriesApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllCategories: builder.query<PaginatedResponse<Category>, string>({
      query: params => {
        return `api/categories?${params}`
      }
    })
  })
})

export const { useGetAllCategoriesQuery } = categoriesApi

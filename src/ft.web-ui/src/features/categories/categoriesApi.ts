import { createApi } from '@reduxjs/toolkit/query/react'
import { baseQueryWithAuth } from '@/lib/customBaseQuery'

import { Category } from '@/types/categories'
import { PaginatedResponse } from '@/types/paginated'

type CategoriesQueryParameters = {
  currentPage?: number
  pageSize?: number
}

export const categoriesApi = createApi({
  reducerPath: 'categoriesApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getAllCategories: builder.query<PaginatedResponse<Category>, CategoriesQueryParameters>({
      query: params => {
        let paramsAsString: Record<string, string> = {}

        for (let [key, value] of Object.entries(params)) {
          paramsAsString[key] = value?.toString() || ''
        }

        const queryParams = new URLSearchParams(paramsAsString).toString()
        return `api/categories?${queryParams}`
      }
    })
  })
})

export const { useGetAllCategoriesQuery } = categoriesApi

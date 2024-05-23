import Cookies from 'js-cookie'
import {
  BaseQueryFn,
  FetchArgs,
  FetchBaseQueryError,
  fetchBaseQuery
} from '@reduxjs/toolkit/query/react'

export const baseQueryWithAuth: BaseQueryFn<
  string | FetchArgs,
  unknown,
  FetchBaseQueryError,
  {},
  {}
> = async (args, api, extraOptions) => {
  const accessToken = Cookies.get('accessToken')
  const refreshToken = Cookies.get('refreshToken')

  let headers: Record<string, string> = {}
  if (typeof args === 'string') {
    args = { url: args }
  }

  headers = {
    ...headers,
    Authorization: accessToken ? `Bearer ${accessToken}` : '',
    'x-refresh': refreshToken || ''
  }

  if (args.headers) {
    args.headers = {
      ...args.headers,
      ...headers
    }
  } else {
    args.headers = headers
  }

  const baseQuery = fetchBaseQuery({
    baseUrl: 'http://localhost:5000'
  })

  return baseQuery(args, api, extraOptions)
}

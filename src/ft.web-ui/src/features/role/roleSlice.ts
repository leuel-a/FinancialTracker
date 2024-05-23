import Cookies from 'js-cookie'
import {
  BaseQueryFn,
  createApi,
  FetchArgs,
  fetchBaseQuery,
  FetchBaseQueryError
} from '@reduxjs/toolkit/query/react'
import { Role } from '@/types/role'

const baseQueryWithAuth: BaseQueryFn<
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
    baseUrl: 'http://localhost:5000/api/roles'
  })

  return baseQuery(args, api, extraOptions)
}

const roleApi = createApi({
  reducerPath: 'roleApi',
  baseQuery: baseQueryWithAuth,
  endpoints: builder => ({
    getRoles: builder.query<Role[], void>({ query: () => '' })
  })
})

export const { useGetRolesQuery } = roleApi
export default roleApi

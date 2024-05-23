import { cookies } from 'next/headers'


export function isAuthenticated() {
  const cookiesStore = cookies()

  const accessToken = cookiesStore.get('accessToken')
  return accessToken !== undefined
}



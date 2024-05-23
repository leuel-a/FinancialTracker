import { NextRequest, NextResponse } from 'next/server'
import { isAuthenticated } from './lib/auth.server'

const excludedRoutes = ['/']

export default function middleware(request: NextRequest) {
  const {
    nextUrl: { pathname }
  } = request

  const isAuth = isAuthenticated()
  if (!isAuth && !excludedRoutes.includes(pathname)) {
    return NextResponse.redirect(new URL('/', request.url))
  }

  const requestHeaders = new Headers(request.headers)
  requestHeaders.set('x-url', request.url)

  return NextResponse.next({
    request: {
      headers: requestHeaders
    }
  })
}

export const config = {
  matcher: '/dashboard/:path*'
}

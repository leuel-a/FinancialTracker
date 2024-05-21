import { NextRequest, NextResponse } from 'next/server'
import { isAuthenticated } from './lib/isAuthenticated'

const excludedRoutes = ['/']

export default function middleware(request: NextRequest) {
  const {nextUrl: { pathname }} = request

  const isAuth = isAuthenticated()
  if (!isAuth && !excludedRoutes.includes(pathname)) {
    return NextResponse.redirect(new URL('/', request.url))
  }
}

export const config = {
  matcher: '/dashboard/:path*'
}

'use client'

import { useWindowWidth } from '@react-hook/window-size'

import { jwtDecode, JwtPayload } from 'jwt-decode'
import Cookies from 'js-cookie'
import { useState, useEffect } from 'react'
import { Nav } from './ui/nav'
import { Button } from '@/components/ui/button'

import {
  ArrowLeftRight,
  LayoutDashboard,
  Calendar,
  Activity,
  ChevronRight,
  User,
  ChevronLeft,
  Settings,
  LogOut
} from 'lucide-react'

interface ExtendedJwtPayload extends JwtPayload {
  'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'?: string
}

export default function SideNavBar() {
  const [isAdmin, setIsAdmin] = useState<boolean>(false)
  const [isCollapsed, setIsCollapsed] = useState<boolean>(false)

  useEffect(() => {
    const accessToken = Cookies.get('accessToken')
    const decodedAccessToken = jwtDecode<ExtendedJwtPayload>(accessToken as string)

    console.log(decodedAccessToken)

    const roleClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    if (decodedAccessToken[roleClaim] === 'Admin') {
      setIsAdmin(true)
    }
  }, [])

  const onlyWidth = useWindowWidth()
  const mobile = onlyWidth < 768

  function toogleSideBar() {
    setIsCollapsed(prev => !prev)
  }

  return (
    <div className="relative flex min-w-20 flex-col justify-between border-r px-3 pb-10 pt-24 transition-all">
      {!mobile && (
        <div className="absolute -right-6 top-7">
          <Button
            onClick={toogleSideBar}
            variant="secondary"
            className="rounded-full border border-black shadow-lg"
          >
            {!isCollapsed ? <ChevronLeft size={14} color="black" /> : <ChevronRight size={14} />}
          </Button>
        </div>
      )}
      <Nav
        links={[
          {
            title: 'Dashboard',
            icon: LayoutDashboard,
            variant: 'default',
            href: '/dashboard'
          },
          {
            title: 'Transactions',
            icon: ArrowLeftRight,
            variant: 'ghost',
            href: '/dashboard/transactions'
          },
          {
            title: 'Calendar',
            icon: Calendar,
            variant: 'ghost',
            href: '/dashboard/calendar'
          },
          {
            title: 'Analysis',
            icon: Activity,
            variant: 'ghost',
            href: '/dashboard/analysis'
          },
          {
            title: 'Employees',
            icon: User,
            variant: 'ghost',
            href: '/dashboard/employees'
          }
        ]}
        isCollapsed={mobile ? true : isCollapsed}
      />
      {isAdmin ? (
        <Nav
          isCollapsed={mobile ? true : isCollapsed}
          links={[
            {
              title: 'Settings',
              icon: Settings,
              variant: 'ghost',
              href: '/dashboard/admin/settings'
            }
          ]}
        />
      ) : (
        <Nav
          isCollapsed={mobile ? true : isCollapsed}
          links={[
            {
              title: 'Logout',
              icon: LogOut,
              variant: 'ghost',
              href: '/logout'
            }
          ]}
        />
      )}
    </div>
  )
}

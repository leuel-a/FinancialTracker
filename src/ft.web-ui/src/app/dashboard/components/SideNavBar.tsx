'use client'

import { useWindowWidth } from '@react-hook/window-size'

import { useState } from 'react'
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
  Settings
} from 'lucide-react'

export default function SideNavBar() {
  const [isCollapsed, setIsCollapsed] = useState<boolean>(false)

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
    </div>
  )
}

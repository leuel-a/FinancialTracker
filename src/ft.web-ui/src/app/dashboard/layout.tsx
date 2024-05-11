import React from 'react'
import SideNavbar from './components/SideNavBar'
import { cn } from '@/lib/utils'

interface DashboardLayoutProps {
  children: React.ReactNode
}

export default function DashboardLayout({ children }: DashboardLayoutProps) {
  return (
    <div className={cn('flex min-h-screen w-full bg-white text-black')}>
      <SideNavbar /> 
      <div className="w-full p-10 pt-8">{children}</div>
    </div>
  )
}

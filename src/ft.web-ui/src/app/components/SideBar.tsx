'use client'

import React from 'react'

import Link from 'next/link'
import Image from 'next/image'

// React Icons Imports
import { IoAnalytics } from 'react-icons/io5'
import { HiOutlineHome } from 'react-icons/hi'
import { GrTransaction } from 'react-icons/gr'
import { IoPeopleOutline } from 'react-icons/io5'
import { IoCalendarOutline } from 'react-icons/io5'
import { IoSettingsOutline } from 'react-icons/io5'
import { IoIosHelpCircleOutline } from 'react-icons/io'
import { IoPersonCircleOutline } from 'react-icons/io5'

import { DashboardLink } from '@/app/types/dashboard'
import SideBarLink from '@/app/components/SideBarLink'

// TODO: Refactor this to use a more dynamic approach, maybe from a config file
const dashboardLinks: DashboardLink[] = [
  {
    route: '/',
    name: 'Dashboard',
    Icon: HiOutlineHome
  },
  {
    route: '/analytics',
    name: 'Analytics',
    Icon: IoAnalytics
  },
  {
    route: '/transactions',
    name: 'Transactions',
    Icon: GrTransaction
  },
  {
    route: 'calendar',
    name: 'Calendar',
    Icon: IoCalendarOutline
  },
  {
    route: '/employee',
    name: 'Employee',
    Icon: IoPeopleOutline
  }
]

const dashboardOtherLinks: DashboardLink[] = [
  {
    route: '/settings',
    name: 'Settings',
    Icon: IoSettingsOutline
  },
  {
    route: '/help',
    name: 'Help',
    Icon: IoIosHelpCircleOutline
  }
]

export default function SideBar() {
  return (
    <aside className="flex w-1/5 flex-col justify-between bg-zinc-200 px-7 py-10">
      <div className="flex flex-col space-y-10">
        <div>
          <Image
            className="mx-auto"
            src="/images/logo-no-background.png"
            alt="Finacial Tracker"
            width={200}
            height={20}
          />
        </div>
        <div>
          <h2 className="text-[13px] text-gray-500">MAIN</h2>
          <div className="mt-4 flex flex-col space-y-4">
            {dashboardLinks.map(link => (
              <SideBarLink key={link.route} data={link} />
            ))}
          </div>
        </div>
        <div>
          <h2 className="text-[13px] text-gray-500">OTHER</h2>
          <div className="mt-4 flex flex-col space-y-4">
            {dashboardOtherLinks.map(link => (
              <SideBarLink key={link.route} data={link} />
            ))}
          </div>
        </div>
      </div>
      <div className="flex items-center justify-start space-x-2">
        <IoPersonCircleOutline className="text-3xl text-gray-600" />
        <h2 className="mt-2 text-lg text-gray-600">John Doe</h2>
      </div>
    </aside>
  )
}

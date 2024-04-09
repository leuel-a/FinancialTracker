'use client'

import Link from 'next/link'
import Image from 'next/image'

import { IoAnalytics } from 'react-icons/io5'
import { HiOutlineHome } from 'react-icons/hi'
import { GrTransaction } from 'react-icons/gr'
import { IoCalendarOutline } from 'react-icons/io5'

export default function SideBar() {
  return (
    <aside className="flex w-1/5 flex-col space-y-2  px-7 py-10 ">
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
        <div>
          <h2 className="text-[13px] text-gray-500">MAIN</h2>
          <div className="mt-2 flex flex-col space-y-4">
            <div>
              <div className="flex items-center space-x-2">
                <HiOutlineHome className="inline-block text-gray-600" />
                <Link className="text-gray-600" href="/">
                  Dashboard
                </Link>
              </div>
              <div className="flex items-center space-x-2">
                <IoAnalytics className="inline-block text-gray-600" />
                <Link className="text-gray-600" href="/analytics">
                  Analytics
                </Link>
              </div>
              <div className="flex items-center space-x-2">
                <GrTransaction className="inline-block text-gray-600" />
                <Link className="text-gray-600" href="/transactions">
                  Transactions
                </Link>
              </div>
              <div className="flex items-center space-x-2">
                <IoCalendarOutline className="inline-block text-gray-600" />
                <Link className="text-gray-600" href="/calendar">
                  Calendar
                </Link>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div>
        <div>
          <h2 className="text-[13px] text-gray-500">OTHER</h2>
          <ul className="mt-2 flex flex-col space-y-4">
            <li>
              <Link className="text-gray-600" href="/">
                Settings
              </Link>
            </li>
            <li>
              <Link className="text-gray-600" href="/">
                Help
              </Link>
            </li>
          </ul>
        </div>
      </div>
    </aside>
  )
}

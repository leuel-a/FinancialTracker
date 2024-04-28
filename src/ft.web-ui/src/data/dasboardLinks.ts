import { SlLogout } from 'react-icons/sl'
import { RxDashboard } from 'react-icons/rx'
import { CiCalendarDate } from 'react-icons/ci'
import { FaPeopleGroup } from 'react-icons/fa6'
import { SiSimpleanalytics } from 'react-icons/si'
import { IoSettingsOutline } from 'react-icons/io5'
import { IoNewspaperOutline } from 'react-icons/io5'

import { Link } from '@/types/link'

export const links: Link[] = [
  { name: 'Dashboard', Icon: RxDashboard, route: '/' },
  { name: 'Analytics', Icon: SiSimpleanalytics, route: '/analytics' },
  { name: 'Employees', Icon: FaPeopleGroup, route: '/employees' },
  { name: 'Calendar', Icon: CiCalendarDate, route: '/calendar' },
  { name: 'Reports', Icon: IoNewspaperOutline, route: '/reports' }
]

export const bottomLinks: Link[] = [
  { name: 'Settings', route: '/settings', Icon: IoSettingsOutline },
  { name: 'Logout', route: '/logout', Icon: SlLogout }
]

import { SlLogout } from 'react-icons/sl'
import { RxDashboard } from 'react-icons/rx'
import { CiCalendarDate } from 'react-icons/ci'
import { FaPeopleGroup } from 'react-icons/fa6'
import { SiSimpleanalytics } from 'react-icons/si'
import { IoSettingsOutline } from 'react-icons/io5'
import { IoNewspaperOutline } from 'react-icons/io5'

import { Link } from '@/types/link'

export const links: Link[] = [
  { name: 'Dashboard', Icon: RxDashboard},
  { name: 'Analytics', Icon: SiSimpleanalytics },
  { name: 'Employees', Icon: FaPeopleGroup },
  { name: 'Calendar', Icon: CiCalendarDate },
  { name: 'Reports', Icon: IoNewspaperOutline }
]

export const bottomLinks: Link[] = [
  { name: 'Settings', Icon: IoSettingsOutline },
  { name: 'Logout', Icon: SlLogout }
]

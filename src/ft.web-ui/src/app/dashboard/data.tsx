import {
  ArrowLeftRight,
  LayoutDashboard,
  Calendar,
  Activity,
  ChevronRight,
  User,
  ChevronLeft,
  Settings,
  LogOut,
  LucideProps
} from 'lucide-react'

type Link = {
  title: string
  label?: string
  icon: React.ForwardRefExoticComponent<
    React.PropsWithoutRef<LucideProps> & React.RefAttributes<SVGSVGElement>
  >
  variant: 'default' | 'ghost'
  href: string
}

const topLinks: Link[] = [
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
  }
]

const bottomLinks: Link[] = [
  {
    title: 'Log Out',
    icon: LogOut,
    variant: 'ghost',
    href: '/logout'
  }
]

const adminTopLinks: Link[] = [
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
]

const adminBottomLinks: Link[] = [
  {
    title: 'Settings',
    icon: Settings,
    variant: 'ghost',
    href: '/dashboard/admin/settings'
  },
  {
    title: 'Log Out',
    icon: LogOut,
    variant: 'ghost',
    href: '/logout'
  }
]

export { topLinks, bottomLinks, adminTopLinks, adminBottomLinks }
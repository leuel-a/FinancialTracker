import React from 'react'
import PageTitle from './components/PageTitle'

import BarChart from './components/BarChart'
import { SalesCardProps } from './components/SalesCard'
import Card, { CardContent, CardProps } from './components/Card'
import { Activity, CreditCard, DollarSign, Users } from 'lucide-react'
import CashFlowDashboard from '@/app/dashboard/components/CashFlowDashboard'

const cardData: CardProps[] = [
  {
    label: 'Total Revenue',
    amount: '$45,234.65',
    description: 'Total revenue generated this month',
    icon: DollarSign
  },
  {
    label: 'Subscriptions',
    amount: '1,234',
    description: '+12% from last month',
    icon: Users
  },
  {
    label: 'Sales',
    amount: '+2,345',
    description: '+19% from last month',
    icon: CreditCard
  },
  {
    label: 'Active Now',
    amount: '+345',
    description: '+19% from last month',
    icon: Activity
  }
]

const salesData: SalesCardProps[] = [
  { name: 'John Doe', email: 'john.doe@example.com', salesAmount: '$1000' },
  { name: 'Jane Smith', email: 'jane.smith@example.com', salesAmount: '$2000' },
  { name: 'Bob Johnson', email: 'bob.johnson@example.com', salesAmount: '$1500' },
  { name: 'Alice Williams', email: 'alice.williams@example.com', salesAmount: '$3000' },
  { name: 'Charlie Brown', email: 'charlie.brown@example.com', salesAmount: '$2500' }
]

export default function DashboardPage() {
  // TODO: get the user from the database and add it to the page header
  // TODO: refactor the cards that are going to appear in the dashboard, add more meaning to them

  return (
    <div className="flex w-full flex-col gap-5">
      <div>
        <PageTitle className="font-bold" title="Dashboard" />
        <p className="text-md font-semibold text-gray-600">Welcome back, Leuel</p>
      </div>
      <section className="grid w-full grid-cols-1 gap-4 gap-x-8 transition-all sm:grid-cols-2 xl:grid-cols-4">
        {cardData.map((card, index) => (
          <Card key={index} {...card} />
        ))}
      </section>
      <section className="grid grid-cols-1 gap-4 transition-all lg:grid-cols-2">
        <CardContent>
          <p className="p-4 font-semibold ">Overview</p>
          <BarChart />
        </CardContent>
        <CardContent>
          <CashFlowDashboard />
        </CardContent>
      </section>
    </div>
  )
}

{/*<CardContent className="flex flex-col justify-between gap-4">*/}
{/*  <section>*/}
{/*    <p>Recent Sales</p>*/}
{/*    <p className="text-sm text-gray-400">You have made 256 sales this month.</p>*/}
{/*  </section>*/}
{/*  {salesData.map((data, index) => (*/}
{/*    <SalesCard*/}
{/*      key={index}*/}
{/*      email={data.email}*/}
{/*      name={data.name}*/}
{/*      salesAmount={data.salesAmount}*/}
{/*    />*/}
{/*  ))}*/}
{/*</CardContent>*/}
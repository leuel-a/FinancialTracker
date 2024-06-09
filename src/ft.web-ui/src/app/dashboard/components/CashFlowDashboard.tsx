import React from 'react'
import LineChartComponent from '@/app/dashboard/components/LineChart'

export default function CashFlowDashboard() {
  return (
    <>
      <div className="flex justify-between items-center">
        <p className="p-4 font-semibold">Cash Flow</p>
        <div
          className="flex h-8 justify-evenly items-center border border-zinc-700 w-fit px-2 gap-4 rounded-md text-sm shadow-md">
          <div>12 Months</div>
          <div className="border-l h-full border-zinc-700"></div>
          <div>30 Days</div>
          <div className="border-l h-full border-zinc-700"></div>
          <div>7 Days</div>
          <div className="border-l h-full border-zinc-700"></div>
          <div>24 Hours</div>
        </div>
      </div>
      <LineChartComponent /></>
  )
}
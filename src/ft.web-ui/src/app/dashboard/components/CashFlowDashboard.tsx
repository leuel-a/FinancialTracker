'use client'

import React from 'react'
import LineChartComponent from '@/app/dashboard/components/LineChart'
import SingleMonthLineChart from '@/app/dashboard/components/SingleMonthLineChart'
import SingleMonthAreaChart from '@/app/dashboard/components/SingleMonthAreaChart'

export default function CashFlowDashboard() {
  const [selected, setSelected] = React.useState<number>(0)
  const charts = [<SingleMonthAreaChart month={1} year={2024} />, <LineChartComponent />]

  return (
    <>
      <div className="flex justify-between items-center">
        <p className="p-4 font-semibold">Cash Flow</p>
        <div
          className="flex h-8 justify-evenly items-center border border-zinc-700 w-fit px-2 gap-4 rounded-md text-sm shadow-md">
          <div className={`cursor-pointer ${selected == 0 && 'underline'}`} onClick={() => setSelected(0)}>30 Days</div>
          <div className="border-l h-full border-zinc-700"></div>
          <div className={`cursor-pointer ${selected == 1 && 'underline'}`} onClick={() => setSelected(1)}>12 Months</div>
        </div>
      </div>
      {charts[selected as number]}
    </>
  )
}
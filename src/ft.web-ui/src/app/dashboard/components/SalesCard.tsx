import React from 'react'

export type SalesCardProps = {
  name: string
  email: string
  salesAmount: string
}

export default function SalesCard({ name, email, salesAmount }: SalesCardProps) {
  return (
    <div className="flex flex-wrap justify-between gap-3">
      <section className="flex justify-between gap-3">
        <div className="h-12 w-12 rounded-full bg-gray-100 p-1">
          <img
            width={200}
            height={200}
            src={`https://api.dicebear.com/8.x/lorelei/svg?seed=${name}`}
            alt="avatar"
          />
        </div>
        <div className="text-sm">
          <p>{name}</p>
          <div className="w-[120px] overflow-hidden text-ellipsis whitespace-nowrap text-gray-400 sm:w-auto">
            {email}
          </div>
        </div>
      </section>
      <p>{salesAmount}</p>
    </div>
  )
}

type CalendarCellProps = { day: number }

const CalendarCell: React.FC<CalendarCellProps> = ({ day }) => {
  return <div className="h-32 rounded-md border border-gray-200">{day}</div>
}

export default CalendarCell

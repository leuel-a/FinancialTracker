interface ErrorMessageProps extends React.HTMLAttributes<HTMLDivElement> {
  message: string
}

export default function ErrorMessage({ message, ...props }: ErrorMessageProps) {
  return (
    <div {...props}>
      <p className="text-sm text-red-500">{message}</p>
    </div>
  )
}
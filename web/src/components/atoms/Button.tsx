export const Button = ({
  children,
  onClick = () => { },
}: {
  children: string,
  onClick?: () => void,
}) => {
  return (
    <button
      className="rounded-lg text-center text-white bg-blue-700 hover:bg-blue-800 px-4 py-2 focus:outline-none"
      onClick={onClick}
    >
      {children}
    </button>
  );
};

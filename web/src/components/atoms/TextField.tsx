
export const TextField = ({
  label,
}: {
  label: string,
}) => {
  return (
    <label className="flex flex-col gap-2">
      <span>{label}</span>
      <input className="border rounded-lg px-2 py-1 focus:outline-none focus:border-shadow" />
    </label>
  );
};

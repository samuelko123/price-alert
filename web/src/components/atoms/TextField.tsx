"use client";

export const TextField = ({
  label,
  value,
  onChange,
}: {
  label: string,
  value: string,
  onChange: (value: string) => void
}) => {
  return (
    <label className="w-full flex flex-col gap-2">
      <span>{label}</span>
      <input
        className="border rounded-lg px-2 py-1 focus:outline-none focus:border-shadow"
        value={value}
        onChange={(event) => onChange(event.target.value)}
      />
    </label>
  );
};

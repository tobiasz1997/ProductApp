export function removeOuterQuotes(str: string): string {
  return str.startsWith('"') && str.endsWith('"')
    ? str.slice(1, -1)
    : str;
}

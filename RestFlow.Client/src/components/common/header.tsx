"use client";
import { cn } from "@/lib/class-names";
import Link from "next/link";
import { Button } from "@/components/ui/button";
import { usePathname } from "next/navigation";
import Typography from "@/components/ui/typography";

interface SidebarProps extends React.HTMLAttributes<HTMLDivElement> {}

export function Header({ className }: SidebarProps) {
  const pathname = usePathname();
  const items = [
    {
      href: "/",
      title: "Book a demo",
      openInNewTab: false,
    },
    // { href: '#pricing', title: 'Features' },
    // {
    //   href: 'mailto:myemail@.com',
    //   title: 'Contact Us'
    // }
  ];

  const getLogo = () => (
    <Link href="/" className="pointer flex items-center">
      <img src="/logo.png" className="mr-3 h-8" />
    </Link>
  );

  const getAuthButtons = () => (
    <div className="flex gap-3 items-center">
      <Link href="/login">
        <Typography variant="p">Login</Typography>
      </Link>
      <Link href="/login">
        <Button size="tiny" color="ghost">
          <Typography variant="p" className="text-black">
            Sign Up
          </Typography>
        </Button>
      </Link>
    </div>
  );

  const getHeaderItems = () => {
    return (
      <>
        {items.map((item) => {
          const selected =
            pathname === item.href || pathname.includes(item.href);
          return (
            <Link
              href={item.href}
              className="pointer block w-fit"
              target={item.openInNewTab ? "_blank" : ""}
              key={item.title}
            >
              <Typography
                variant="p"
                className={cn(selected && "text-primary")}
              >
                {item.title}
              </Typography>
            </Link>
          );
        })}
      </>
    );
  };

  return (
    <div
      className={cn(
        `flex md:h-12 h-14 items-center justify-center w-full
          border-b`,
        className
      )}
    >
      <div className="w-full max-w-[1280px] md:px-8 px-4">
        {/* Desktop */}
        <div className="flex items-center gap-x-8 w-full">
          <div className="md:flex-0 min-w-fit flex-1">{getLogo()}</div>
          <div className="hidden md:flex flex items-center w-full">
            <div className="flex items-center gap-x-8 flex-1">
              {getHeaderItems()}
            </div>
            {getAuthButtons()}
          </div>
          {/* Mobile */}
          <div className="md:hidden flex gap-x-4 items-center">
            {getAuthButtons()}
          </div>
        </div>
      </div>
    </div>
  );
}

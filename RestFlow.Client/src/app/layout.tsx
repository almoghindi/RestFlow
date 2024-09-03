import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import { ThemeProvider } from "@/providers/theme-provider";
import QueryProvider from "@/providers/query-provider";
import ReduxProvider from "@/providers/redux-provider";
import { Header } from "@/components/common/header";
import { Footer } from "@/components/common/footer";
import { Analytics } from "@vercel/analytics/react";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "RestFlow",
  description:
    "Instantly connect new orders and customer requests to previous orders and solutions. All seamlessly integrated into your restaurant management system the moment an order is placed.",
  openGraph: {
    images: "/hero.png",
  },
  twitter: {
    card: "summary_large_image",
    title: "RestFlow - Your All-in-One Platform for Restaurant Efficiency",
    description:
      "Instantly connect new orders and customer requests to previous orders and solutions. All seamlessly integrated into your restaurant management system the moment an order is placed.",
    images: ["./icon.png"],
  },
  icons: {
    icon: "/favicon.ico",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" suppressHydrationWarning className="antialiased">
      <head>
        <link
          rel="icon"
          type="image/png"
          sizes="32x32"
          href="/favicon-32x32.png"
        />
      </head>
      <Analytics />
      <body className={inter.className}>
        <ReduxProvider>
          <QueryProvider>
            <ThemeProvider
              attribute="class"
              defaultTheme="dark"
              enableSystem
              disableTransitionOnChange
            >
              <main className={`flex min-h-screen flex-col ${inter.className}`}>
                <div className="flex flex-1 justify-center w-full">
                  <div className="flex h-full">{children}</div>
                </div>
                <Footer />
              </main>
            </ThemeProvider>
          </QueryProvider>
        </ReduxProvider>
      </body>
    </html>
  );
}

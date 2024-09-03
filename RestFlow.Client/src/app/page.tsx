"use client";
import { Button } from "@/components/ui/button";
import Typography from "@/components/ui/typography";
import Image from "next/image";
import Feature from "./feature";
import { ArrowUpDown, Timer, Workflow } from "lucide-react";
import Link from "next/link";
import ContactForm from "./contact-form";
import useAuthRedirect from "@/hooks/use-auth-redirect";
import { Header } from "@/components/common/header";

export default function Home() {
  useAuthRedirect();

  return (
    <div className="flex flex-col">
      <Header />
      <div
        className="flex flex-col h-full md:py-36 md:px-32 pt-11 pb-24 px-8
      w-full items-center text-center gap-12"
      >
        <div className="flex flex-col gap-6 items-center">
          <Typography className="max-w-2xl" variant="h1">
            Your All-in-One Platform for Restaurant Efficiency
          </Typography>
          <Typography className="max-w-2xl" variant="h5">
            Instantly connect new orders and customer requests to previous
            orders and solutions. All seamlessly integrated into your restaurant
            management system the moment an order is placed.
          </Typography>
          <Link
            href="https://map.sistilli.dev/public/coding/SaaS+Boilerplate"
            target="_blank"
          >
            <Button size="tiny" variant="ghost">
              {`Get Started`}
            </Button>
          </Link>
          <Image width={1024} height={632} alt="Hero image" src="/hero.png" />
        </div>
        <div className="flex flex-col md:pt-24 md:gap-36 gap-24 items-center">
          <div className="flex flex-col gap-12 items-center">
            <Typography className="max-w-2xl" variant="h1">
              Quick solutions, less stress
            </Typography>
            <div className="flex md:flex-row flex-col gap-12">
              <Feature
                icon={<Timer size={24} />}
                headline="Fix emergencies fast"
                description="Save 20-30 minutes per on-call ticket - no more searching for relevant issues and runbooks"
              />
              <Feature
                icon={<ArrowUpDown size={24} />}
                headline="Universally compatible"
                description="Works with TabIt, Testigo, or beecomm integrates with any system"
              />
              <Feature
                icon={<Workflow size={24} />}
                headline="Secure for your org"
                description="We keep your data safe by taking top security measures."
              />
            </div>
          </div>
          <div className="flex flex-col gap-6 max-w-2xl items-center">
            <Typography className="max-w-2xl" variant="h1">
              Instant setup, no custom code
            </Typography>
            <Typography className="max-w-2xl" variant="p">
              Quickly link new on-call tickets to similar past incidents and
              their solutions. All directly in Slack the moment an incident
              happens.
            </Typography>
            <Image
              width={1024}
              height={632}
              alt="Hero image"
              src="/hero1.png"
            />
          </div>
          <ContactForm />
        </div>
      </div>
    </div>
  );
}

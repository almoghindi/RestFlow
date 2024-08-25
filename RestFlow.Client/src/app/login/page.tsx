"use client";

import React from "react";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import Typography from "@/components/ui/typography";
import useRequest from "@/hooks/use-request";
import { LoadingSpinner } from "@/components/ui/loading-spinner";
import { useDispatch } from "react-redux";
import { setRestaurant } from "@/store/slices/restaurant-slice";

const schema = z.object({
  name: z.string().min(1, "Name is required"),
  password: z.string().min(6, "Password must be at least 6 characters long"),
});

type LoginFormData = z.infer<typeof schema>;

const LoginPage = () => {
  const dispatch = useDispatch();
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(schema),
    defaultValues: {
      name: "",
      password: "",
    },
  });

  const { sendRequest, isLoading, requestErrors } = useRequest();

  const onSubmit = async (data: LoginFormData) => {
    console.log(data);
    await sendRequest({
      url: "/api/restaurant/login",
      method: "POST",
      body: data,
      onSuccess: (responseData) => {
        dispatch(
          setRestaurant({ id: responseData.id, name: responseData.name })
        );
        console.log("Restaurant logged in:", responseData);
      },
    });
  };

  return (
    <>
      {isLoading && <LoadingSpinner />}
      <div className="flex mt-16 justify-center min-h-screen">
        <form
          onSubmit={handleSubmit(onSubmit)}
          className="p-8 rounded-lg shadow-md"
        >
          <Typography className="max-w-2xl m-8" variant="h1">
            Restaurant Login
          </Typography>
          <div className="mb-4">
            <label htmlFor="username" className="block text-sm font-medium">
              Restaurant Name:
            </label>
            <Controller
              name="name"
              control={control}
              render={({ field }) => <Input id="name" size="lg" {...field} />}
            />
            {errors.name && (
              <p className="mt-1 text-sm text-red-600">{errors.name.message}</p>
            )}
          </div>
          <div className="mb-4">
            <label htmlFor="password" className="block text-sm font-medium">
              Password:
            </label>
            <Controller
              name="password"
              control={control}
              render={({ field }) => (
                <Input type="password" id="password" size="lg" {...field} />
              )}
            />
            {errors.password && (
              <p className="mt-1 text-sm text-red-600">
                {errors.password.message}
              </p>
            )}
          </div>
          {requestErrors &&
            requestErrors.map((error) => (
              <div key={error.message} className="text-red-500">
                {error.message}
              </div>
            ))}

          <Button type="submit" variant="ghost" size="lg" className="w-full">
            Login
          </Button>
        </form>
      </div>
    </>
  );
};

export default LoginPage;

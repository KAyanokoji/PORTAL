import NotificationProvider from "@/components/common/control/SetupSnackbar.";
import "./globals.css";
import { Providers } from "@/redux/provider";
import AppWrapper from "@/utils/AppWrapper";


export const metadata = {
  title: "Create Next App",
  description: "Generated by create next app",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      <body
        className=''
      >
        <Providers>
        <NotificationProvider>
          <AppWrapper>
            {children}
          </AppWrapper>
          </NotificationProvider>
        </Providers>
      </body>
    </html>
  );
}

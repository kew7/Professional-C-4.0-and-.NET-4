﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace P2PSample
{
   // NOTE: If you change the class name "P2PService" here, you must also update the reference to "P2PService" in App.config.
   [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
   public class P2PService : IP2PService
   {
      private Window1 hostReference;
      private string username;

      public P2PService(Window1 hostReference, string username)
      {
         this.hostReference = hostReference;
         this.username = username;
      }

      public string GetName()
      {
         return username;
      }

      public void SendMessage(string message, string from)
      {
         hostReference.DisplayMessage(message, from);
      }
   }
}

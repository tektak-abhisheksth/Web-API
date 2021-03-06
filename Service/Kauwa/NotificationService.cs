/**
 * Autogenerated by Thrift Compiler (0.9.2)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace TekTak.iLoop.Kauwa
{
  public partial class NotificationService {
    public interface Iface {
      int messageCount(Session session);
      #if SILVERLIGHT
      IAsyncResult Begin_messageCount(AsyncCallback callback, object state, Session session);
      int End_messageCount(IAsyncResult asyncResult);
      #endif
      DbStatus setNotificationAndRequest(int userId, string notificationIds, int filterType, bool clicked, Session session);
      #if SILVERLIGHT
      IAsyncResult Begin_setNotificationAndRequest(AsyncCallback callback, object state, int userId, string notificationIds, int filterType, bool clicked, Session session);
      DbStatus End_setNotificationAndRequest(IAsyncResult asyncResult);
      #endif
    }

    public class Client : IDisposable, Iface {
      public Client(TProtocol prot) : this(prot, prot)
      {
      }

      public Client(TProtocol iprot, TProtocol oprot)
      {
        iprot_ = iprot;
        oprot_ = oprot;
      }

      protected TProtocol iprot_;
      protected TProtocol oprot_;
      protected int seqid_;

      public TProtocol InputProtocol
      {
        get { return iprot_; }
      }
      public TProtocol OutputProtocol
      {
        get { return oprot_; }
      }


      #region " IDisposable Support "
      private bool _IsDisposed;

      // IDisposable
      public void Dispose()
      {
        Dispose(true);
      }
      

      protected virtual void Dispose(bool disposing)
      {
        if (!_IsDisposed)
        {
          if (disposing)
          {
            if (iprot_ != null)
            {
              ((IDisposable)iprot_).Dispose();
            }
            if (oprot_ != null)
            {
              ((IDisposable)oprot_).Dispose();
            }
          }
        }
        _IsDisposed = true;
      }
      #endregion


      
      #if SILVERLIGHT
      public IAsyncResult Begin_messageCount(AsyncCallback callback, object state, Session session)
      {
        return send_messageCount(callback, state, session);
      }

      public int End_messageCount(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        return recv_messageCount();
      }

      #endif

      public int messageCount(Session session)
      {
        #if !SILVERLIGHT
        send_messageCount(session);
        return recv_messageCount();

        #else
        var asyncResult = Begin_messageCount(null, null, session);
        return End_messageCount(asyncResult);

        #endif
      }
      #if SILVERLIGHT
      public IAsyncResult send_messageCount(AsyncCallback callback, object state, Session session)
      #else
      public void send_messageCount(Session session)
      #endif
      {
        oprot_.WriteMessageBegin(new TMessage("messageCount", TMessageType.Call, seqid_));
        messageCount_args args = new messageCount_args();
        args.Session = session;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        #if SILVERLIGHT
        return oprot_.Transport.BeginFlush(callback, state);
        #else
        oprot_.Transport.Flush();
        #endif
      }

      public int recv_messageCount()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        messageCount_result result = new messageCount_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        if (result.__isset.success) {
          return result.Success;
        }
        if (result.__isset.ne) {
          throw result.Ne;
        }
        if (result.__isset.se) {
          throw result.Se;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "messageCount failed: unknown result");
      }

      
      #if SILVERLIGHT
      public IAsyncResult Begin_setNotificationAndRequest(AsyncCallback callback, object state, int userId, string notificationIds, int filterType, bool clicked, Session session)
      {
        return send_setNotificationAndRequest(callback, state, userId, notificationIds, filterType, clicked, session);
      }

      public DbStatus End_setNotificationAndRequest(IAsyncResult asyncResult)
      {
        oprot_.Transport.EndFlush(asyncResult);
        return recv_setNotificationAndRequest();
      }

      #endif

      public DbStatus setNotificationAndRequest(int userId, string notificationIds, int filterType, bool clicked, Session session)
      {
        #if !SILVERLIGHT
        send_setNotificationAndRequest(userId, notificationIds, filterType, clicked, session);
        return recv_setNotificationAndRequest();

        #else
        var asyncResult = Begin_setNotificationAndRequest(null, null, userId, notificationIds, filterType, clicked, session);
        return End_setNotificationAndRequest(asyncResult);

        #endif
      }
      #if SILVERLIGHT
      public IAsyncResult send_setNotificationAndRequest(AsyncCallback callback, object state, int userId, string notificationIds, int filterType, bool clicked, Session session)
      #else
      public void send_setNotificationAndRequest(int userId, string notificationIds, int filterType, bool clicked, Session session)
      #endif
      {
        oprot_.WriteMessageBegin(new TMessage("setNotificationAndRequest", TMessageType.Call, seqid_));
        setNotificationAndRequest_args args = new setNotificationAndRequest_args();
        args.UserId = userId;
        args.NotificationIds = notificationIds;
        args.FilterType = filterType;
        args.Clicked = clicked;
        args.Session = session;
        args.Write(oprot_);
        oprot_.WriteMessageEnd();
        #if SILVERLIGHT
        return oprot_.Transport.BeginFlush(callback, state);
        #else
        oprot_.Transport.Flush();
        #endif
      }

      public DbStatus recv_setNotificationAndRequest()
      {
        TMessage msg = iprot_.ReadMessageBegin();
        if (msg.Type == TMessageType.Exception) {
          TApplicationException x = TApplicationException.Read(iprot_);
          iprot_.ReadMessageEnd();
          throw x;
        }
        setNotificationAndRequest_result result = new setNotificationAndRequest_result();
        result.Read(iprot_);
        iprot_.ReadMessageEnd();
        if (result.__isset.success) {
          return result.Success;
        }
        if (result.__isset.ne) {
          throw result.Ne;
        }
        if (result.__isset.se) {
          throw result.Se;
        }
        throw new TApplicationException(TApplicationException.ExceptionType.MissingResult, "setNotificationAndRequest failed: unknown result");
      }

    }
    public class Processor : TProcessor {
      public Processor(Iface iface)
      {
        iface_ = iface;
        processMap_["messageCount"] = messageCount_Process;
        processMap_["setNotificationAndRequest"] = setNotificationAndRequest_Process;
      }

      protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);
      private Iface iface_;
      protected Dictionary<string, ProcessFunction> processMap_ = new Dictionary<string, ProcessFunction>();

      public bool Process(TProtocol iprot, TProtocol oprot)
      {
        try
        {
          TMessage msg = iprot.ReadMessageBegin();
          ProcessFunction fn;
          processMap_.TryGetValue(msg.Name, out fn);
          if (fn == null) {
            TProtocolUtil.Skip(iprot, TType.Struct);
            iprot.ReadMessageEnd();
            TApplicationException x = new TApplicationException (TApplicationException.ExceptionType.UnknownMethod, "Invalid method name: '" + msg.Name + "'");
            oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
            x.Write(oprot);
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
            return true;
          }
          fn(msg.SeqID, iprot, oprot);
        }
        catch (IOException)
        {
          return false;
        }
        return true;
      }

      public void messageCount_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        messageCount_args args = new messageCount_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        messageCount_result result = new messageCount_result();
        try {
          result.Success = iface_.messageCount(args.Session);
        } catch (NotificationException ne) {
          result.Ne = ne;
        } catch (SessionException se) {
          result.Se = se;
        }
        oprot.WriteMessageBegin(new TMessage("messageCount", TMessageType.Reply, seqid)); 
        result.Write(oprot);
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

      public void setNotificationAndRequest_Process(int seqid, TProtocol iprot, TProtocol oprot)
      {
        setNotificationAndRequest_args args = new setNotificationAndRequest_args();
        args.Read(iprot);
        iprot.ReadMessageEnd();
        setNotificationAndRequest_result result = new setNotificationAndRequest_result();
        try {
          result.Success = iface_.setNotificationAndRequest(args.UserId, args.NotificationIds, args.FilterType, args.Clicked, args.Session);
        } catch (NotificationException ne) {
          result.Ne = ne;
        } catch (SessionException se) {
          result.Se = se;
        }
        oprot.WriteMessageBegin(new TMessage("setNotificationAndRequest", TMessageType.Reply, seqid)); 
        result.Write(oprot);
        oprot.WriteMessageEnd();
        oprot.Transport.Flush();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class messageCount_args : TBase
    {
      private Session _session;

      public Session Session
      {
        get
        {
          return _session;
        }
        set
        {
          __isset.session = true;
          this._session = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool session;
      }

      public messageCount_args() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.Struct) {
                Session = new Session();
                Session.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("messageCount_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (Session != null && __isset.session) {
          field.Name = "session";
          field.Type = TType.Struct;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          Session.Write(oprot);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("messageCount_args(");
        bool __first = true;
        if (Session != null && __isset.session) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Session: ");
          __sb.Append(Session== null ? "<null>" : Session.ToString());
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class messageCount_result : TBase
    {
      private int _success;
      private NotificationException _ne;
      private SessionException _se;

      public int Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }

      public NotificationException Ne
      {
        get
        {
          return _ne;
        }
        set
        {
          __isset.ne = true;
          this._ne = value;
        }
      }

      public SessionException Se
      {
        get
        {
          return _se;
        }
        set
        {
          __isset.se = true;
          this._se = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool success;
        public bool ne;
        public bool se;
      }

      public messageCount_result() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.I32) {
                Success = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 1:
              if (field.Type == TType.Struct) {
                Ne = new NotificationException();
                Ne.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Struct) {
                Se = new SessionException();
                Se.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("messageCount_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          field.Name = "Success";
          field.Type = TType.I32;
          field.ID = 0;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(Success);
          oprot.WriteFieldEnd();
        } else if (this.__isset.ne) {
          if (Ne != null) {
            field.Name = "Ne";
            field.Type = TType.Struct;
            field.ID = 1;
            oprot.WriteFieldBegin(field);
            Ne.Write(oprot);
            oprot.WriteFieldEnd();
          }
        } else if (this.__isset.se) {
          if (Se != null) {
            field.Name = "Se";
            field.Type = TType.Struct;
            field.ID = 2;
            oprot.WriteFieldBegin(field);
            Se.Write(oprot);
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("messageCount_result(");
        bool __first = true;
        if (__isset.success) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Success: ");
          __sb.Append(Success);
        }
        if (Ne != null && __isset.ne) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Ne: ");
          __sb.Append(Ne== null ? "<null>" : Ne.ToString());
        }
        if (Se != null && __isset.se) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Se: ");
          __sb.Append(Se== null ? "<null>" : Se.ToString());
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class setNotificationAndRequest_args : TBase
    {
      private int _userId;
      private string _notificationIds;
      private int _filterType;
      private bool _clicked;
      private Session _session;

      public int UserId
      {
        get
        {
          return _userId;
        }
        set
        {
          __isset.userId = true;
          this._userId = value;
        }
      }

      public string NotificationIds
      {
        get
        {
          return _notificationIds;
        }
        set
        {
          __isset.notificationIds = true;
          this._notificationIds = value;
        }
      }

      public int FilterType
      {
        get
        {
          return _filterType;
        }
        set
        {
          __isset.filterType = true;
          this._filterType = value;
        }
      }

      public bool Clicked
      {
        get
        {
          return _clicked;
        }
        set
        {
          __isset.clicked = true;
          this._clicked = value;
        }
      }

      public Session Session
      {
        get
        {
          return _session;
        }
        set
        {
          __isset.session = true;
          this._session = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool userId;
        public bool notificationIds;
        public bool filterType;
        public bool clicked;
        public bool session;
      }

      public setNotificationAndRequest_args() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I32) {
                UserId = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.String) {
                NotificationIds = iprot.ReadString();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                FilterType = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.Bool) {
                Clicked = iprot.ReadBool();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 5:
              if (field.Type == TType.Struct) {
                Session = new Session();
                Session.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("setNotificationAndRequest_args");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.userId) {
          field.Name = "userId";
          field.Type = TType.I32;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(UserId);
          oprot.WriteFieldEnd();
        }
        if (NotificationIds != null && __isset.notificationIds) {
          field.Name = "notificationIds";
          field.Type = TType.String;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteString(NotificationIds);
          oprot.WriteFieldEnd();
        }
        if (__isset.filterType) {
          field.Name = "filterType";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(FilterType);
          oprot.WriteFieldEnd();
        }
        if (__isset.clicked) {
          field.Name = "clicked";
          field.Type = TType.Bool;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteBool(Clicked);
          oprot.WriteFieldEnd();
        }
        if (Session != null && __isset.session) {
          field.Name = "session";
          field.Type = TType.Struct;
          field.ID = 5;
          oprot.WriteFieldBegin(field);
          Session.Write(oprot);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("setNotificationAndRequest_args(");
        bool __first = true;
        if (__isset.userId) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("UserId: ");
          __sb.Append(UserId);
        }
        if (NotificationIds != null && __isset.notificationIds) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("NotificationIds: ");
          __sb.Append(NotificationIds);
        }
        if (__isset.filterType) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("FilterType: ");
          __sb.Append(FilterType);
        }
        if (__isset.clicked) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Clicked: ");
          __sb.Append(Clicked);
        }
        if (Session != null && __isset.session) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Session: ");
          __sb.Append(Session== null ? "<null>" : Session.ToString());
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }


    #if !SILVERLIGHT
    [Serializable]
    #endif
    public partial class setNotificationAndRequest_result : TBase
    {
      private DbStatus _success;
      private NotificationException _ne;
      private SessionException _se;

      public DbStatus Success
      {
        get
        {
          return _success;
        }
        set
        {
          __isset.success = true;
          this._success = value;
        }
      }

      public NotificationException Ne
      {
        get
        {
          return _ne;
        }
        set
        {
          __isset.ne = true;
          this._ne = value;
        }
      }

      public SessionException Se
      {
        get
        {
          return _se;
        }
        set
        {
          __isset.se = true;
          this._se = value;
        }
      }


      public Isset __isset;
      #if !SILVERLIGHT
      [Serializable]
      #endif
      public struct Isset {
        public bool success;
        public bool ne;
        public bool se;
      }

      public setNotificationAndRequest_result() {
      }

      public void Read (TProtocol iprot)
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 0:
              if (field.Type == TType.Struct) {
                Success = new DbStatus();
                Success.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 1:
              if (field.Type == TType.Struct) {
                Ne = new NotificationException();
                Ne.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.Struct) {
                Se = new SessionException();
                Se.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }

      public void Write(TProtocol oprot) {
        TStruct struc = new TStruct("setNotificationAndRequest_result");
        oprot.WriteStructBegin(struc);
        TField field = new TField();

        if (this.__isset.success) {
          if (Success != null) {
            field.Name = "Success";
            field.Type = TType.Struct;
            field.ID = 0;
            oprot.WriteFieldBegin(field);
            Success.Write(oprot);
            oprot.WriteFieldEnd();
          }
        } else if (this.__isset.ne) {
          if (Ne != null) {
            field.Name = "Ne";
            field.Type = TType.Struct;
            field.ID = 1;
            oprot.WriteFieldBegin(field);
            Ne.Write(oprot);
            oprot.WriteFieldEnd();
          }
        } else if (this.__isset.se) {
          if (Se != null) {
            field.Name = "Se";
            field.Type = TType.Struct;
            field.ID = 2;
            oprot.WriteFieldBegin(field);
            Se.Write(oprot);
            oprot.WriteFieldEnd();
          }
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }

      public override string ToString() {
        StringBuilder __sb = new StringBuilder("setNotificationAndRequest_result(");
        bool __first = true;
        if (Success != null && __isset.success) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Success: ");
          __sb.Append(Success== null ? "<null>" : Success.ToString());
        }
        if (Ne != null && __isset.ne) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Ne: ");
          __sb.Append(Ne== null ? "<null>" : Ne.ToString());
        }
        if (Se != null && __isset.se) {
          if(!__first) { __sb.Append(", "); }
          __first = false;
          __sb.Append("Se: ");
          __sb.Append(Se== null ? "<null>" : Se.ToString());
        }
        __sb.Append(")");
        return __sb.ToString();
      }

    }

  }
}

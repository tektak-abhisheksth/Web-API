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

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class InboxRule : TBase
  {
    private long _ruleId;
    private int _inboxId;
    private int _typeUserSelection;
    private int _ruleTypeUser;
    private string _entryContacts;
    private string _entryGroups;
    private int _ruleTypeSubject;
    private string _subject;
    private string _createdDate;
    private List<string> _contactList;
    private List<string> _groupList;
    private int _dbStatusCode;
    private bool _applyOnOldMessage;
    private int _dbSubStatusCode;
    private string _dbStatusMsg;
    private List<string> _usernames;

    public long RuleId
    {
      get
      {
        return _ruleId;
      }
      set
      {
        __isset.ruleId = true;
        this._ruleId = value;
      }
    }

    public int InboxId
    {
      get
      {
        return _inboxId;
      }
      set
      {
        __isset.inboxId = true;
        this._inboxId = value;
      }
    }

    public int TypeUserSelection
    {
      get
      {
        return _typeUserSelection;
      }
      set
      {
        __isset.typeUserSelection = true;
        this._typeUserSelection = value;
      }
    }

    public int RuleTypeUser
    {
      get
      {
        return _ruleTypeUser;
      }
      set
      {
        __isset.ruleTypeUser = true;
        this._ruleTypeUser = value;
      }
    }

    public string EntryContacts
    {
      get
      {
        return _entryContacts;
      }
      set
      {
        __isset.entryContacts = true;
        this._entryContacts = value;
      }
    }

    public string EntryGroups
    {
      get
      {
        return _entryGroups;
      }
      set
      {
        __isset.entryGroups = true;
        this._entryGroups = value;
      }
    }

    public int RuleTypeSubject
    {
      get
      {
        return _ruleTypeSubject;
      }
      set
      {
        __isset.ruleTypeSubject = true;
        this._ruleTypeSubject = value;
      }
    }

    public string Subject
    {
      get
      {
        return _subject;
      }
      set
      {
        __isset.subject = true;
        this._subject = value;
      }
    }

    public string CreatedDate
    {
      get
      {
        return _createdDate;
      }
      set
      {
        __isset.createdDate = true;
        this._createdDate = value;
      }
    }

    public List<string> ContactList
    {
      get
      {
        return _contactList;
      }
      set
      {
        __isset.contactList = true;
        this._contactList = value;
      }
    }

    public List<string> GroupList
    {
      get
      {
        return _groupList;
      }
      set
      {
        __isset.groupList = true;
        this._groupList = value;
      }
    }

    public int DbStatusCode
    {
      get
      {
        return _dbStatusCode;
      }
      set
      {
        __isset.dbStatusCode = true;
        this._dbStatusCode = value;
      }
    }

    public bool ApplyOnOldMessage
    {
      get
      {
        return _applyOnOldMessage;
      }
      set
      {
        __isset.applyOnOldMessage = true;
        this._applyOnOldMessage = value;
      }
    }

    public int DbSubStatusCode
    {
      get
      {
        return _dbSubStatusCode;
      }
      set
      {
        __isset.dbSubStatusCode = true;
        this._dbSubStatusCode = value;
      }
    }

    public string DbStatusMsg
    {
      get
      {
        return _dbStatusMsg;
      }
      set
      {
        __isset.dbStatusMsg = true;
        this._dbStatusMsg = value;
      }
    }

    public List<string> Usernames
    {
      get
      {
        return _usernames;
      }
      set
      {
        __isset.usernames = true;
        this._usernames = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool ruleId;
      public bool inboxId;
      public bool typeUserSelection;
      public bool ruleTypeUser;
      public bool entryContacts;
      public bool entryGroups;
      public bool ruleTypeSubject;
      public bool subject;
      public bool createdDate;
      public bool contactList;
      public bool groupList;
      public bool dbStatusCode;
      public bool applyOnOldMessage;
      public bool dbSubStatusCode;
      public bool dbStatusMsg;
      public bool usernames;
    }

    public InboxRule() {
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
            if (field.Type == TType.I64) {
              RuleId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I32) {
              InboxId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              TypeUserSelection = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              RuleTypeUser = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              EntryContacts = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              EntryGroups = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I32) {
              RuleTypeSubject = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.String) {
              Subject = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.String) {
              CreatedDate = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.List) {
              {
                ContactList = new List<string>();
                TList _list44 = iprot.ReadListBegin();
                for( int _i45 = 0; _i45 < _list44.Count; ++_i45)
                {
                  string _elem46;
                  _elem46 = iprot.ReadString();
                  ContactList.Add(_elem46);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.List) {
              {
                GroupList = new List<string>();
                TList _list47 = iprot.ReadListBegin();
                for( int _i48 = 0; _i48 < _list47.Count; ++_i48)
                {
                  string _elem49;
                  _elem49 = iprot.ReadString();
                  GroupList.Add(_elem49);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 12:
            if (field.Type == TType.I32) {
              DbStatusCode = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 13:
            if (field.Type == TType.Bool) {
              ApplyOnOldMessage = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 14:
            if (field.Type == TType.I32) {
              DbSubStatusCode = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 15:
            if (field.Type == TType.String) {
              DbStatusMsg = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 16:
            if (field.Type == TType.List) {
              {
                Usernames = new List<string>();
                TList _list50 = iprot.ReadListBegin();
                for( int _i51 = 0; _i51 < _list50.Count; ++_i51)
                {
                  string _elem52;
                  _elem52 = iprot.ReadString();
                  Usernames.Add(_elem52);
                }
                iprot.ReadListEnd();
              }
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
      TStruct struc = new TStruct("InboxRule");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.ruleId) {
        field.Name = "ruleId";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(RuleId);
        oprot.WriteFieldEnd();
      }
      if (__isset.inboxId) {
        field.Name = "inboxId";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(InboxId);
        oprot.WriteFieldEnd();
      }
      if (__isset.typeUserSelection) {
        field.Name = "typeUserSelection";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(TypeUserSelection);
        oprot.WriteFieldEnd();
      }
      if (__isset.ruleTypeUser) {
        field.Name = "ruleTypeUser";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RuleTypeUser);
        oprot.WriteFieldEnd();
      }
      if (EntryContacts != null && __isset.entryContacts) {
        field.Name = "entryContacts";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(EntryContacts);
        oprot.WriteFieldEnd();
      }
      if (EntryGroups != null && __isset.entryGroups) {
        field.Name = "entryGroups";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(EntryGroups);
        oprot.WriteFieldEnd();
      }
      if (__isset.ruleTypeSubject) {
        field.Name = "ruleTypeSubject";
        field.Type = TType.I32;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RuleTypeSubject);
        oprot.WriteFieldEnd();
      }
      if (Subject != null && __isset.subject) {
        field.Name = "subject";
        field.Type = TType.String;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Subject);
        oprot.WriteFieldEnd();
      }
      if (CreatedDate != null && __isset.createdDate) {
        field.Name = "createdDate";
        field.Type = TType.String;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(CreatedDate);
        oprot.WriteFieldEnd();
      }
      if (ContactList != null && __isset.contactList) {
        field.Name = "contactList";
        field.Type = TType.List;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.String, ContactList.Count));
          foreach (string _iter53 in ContactList)
          {
            oprot.WriteString(_iter53);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (GroupList != null && __isset.groupList) {
        field.Name = "groupList";
        field.Type = TType.List;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.String, GroupList.Count));
          foreach (string _iter54 in GroupList)
          {
            oprot.WriteString(_iter54);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.dbStatusCode) {
        field.Name = "dbStatusCode";
        field.Type = TType.I32;
        field.ID = 12;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(DbStatusCode);
        oprot.WriteFieldEnd();
      }
      if (__isset.applyOnOldMessage) {
        field.Name = "applyOnOldMessage";
        field.Type = TType.Bool;
        field.ID = 13;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(ApplyOnOldMessage);
        oprot.WriteFieldEnd();
      }
      if (__isset.dbSubStatusCode) {
        field.Name = "dbSubStatusCode";
        field.Type = TType.I32;
        field.ID = 14;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(DbSubStatusCode);
        oprot.WriteFieldEnd();
      }
      if (DbStatusMsg != null && __isset.dbStatusMsg) {
        field.Name = "dbStatusMsg";
        field.Type = TType.String;
        field.ID = 15;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(DbStatusMsg);
        oprot.WriteFieldEnd();
      }
      if (Usernames != null && __isset.usernames) {
        field.Name = "usernames";
        field.Type = TType.List;
        field.ID = 16;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.String, Usernames.Count));
          foreach (string _iter55 in Usernames)
          {
            oprot.WriteString(_iter55);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("InboxRule(");
      bool __first = true;
      if (__isset.ruleId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RuleId: ");
        __sb.Append(RuleId);
      }
      if (__isset.inboxId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("InboxId: ");
        __sb.Append(InboxId);
      }
      if (__isset.typeUserSelection) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("TypeUserSelection: ");
        __sb.Append(TypeUserSelection);
      }
      if (__isset.ruleTypeUser) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RuleTypeUser: ");
        __sb.Append(RuleTypeUser);
      }
      if (EntryContacts != null && __isset.entryContacts) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EntryContacts: ");
        __sb.Append(EntryContacts);
      }
      if (EntryGroups != null && __isset.entryGroups) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EntryGroups: ");
        __sb.Append(EntryGroups);
      }
      if (__isset.ruleTypeSubject) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RuleTypeSubject: ");
        __sb.Append(RuleTypeSubject);
      }
      if (Subject != null && __isset.subject) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Subject: ");
        __sb.Append(Subject);
      }
      if (CreatedDate != null && __isset.createdDate) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CreatedDate: ");
        __sb.Append(CreatedDate);
      }
      if (ContactList != null && __isset.contactList) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactList: ");
        __sb.Append(ContactList);
      }
      if (GroupList != null && __isset.groupList) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("GroupList: ");
        __sb.Append(GroupList);
      }
      if (__isset.dbStatusCode) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DbStatusCode: ");
        __sb.Append(DbStatusCode);
      }
      if (__isset.applyOnOldMessage) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ApplyOnOldMessage: ");
        __sb.Append(ApplyOnOldMessage);
      }
      if (__isset.dbSubStatusCode) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DbSubStatusCode: ");
        __sb.Append(DbSubStatusCode);
      }
      if (DbStatusMsg != null && __isset.dbStatusMsg) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DbStatusMsg: ");
        __sb.Append(DbStatusMsg);
      }
      if (Usernames != null && __isset.usernames) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Usernames: ");
        __sb.Append(Usernames);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}

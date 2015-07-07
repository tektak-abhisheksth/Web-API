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
  public partial class ElifImageDetection : TBase
  {
    private double _x;
    private double _y;
    private double _width;
    private double _height;

    public double X
    {
      get
      {
        return _x;
      }
      set
      {
        __isset.x = true;
        this._x = value;
      }
    }

    public double Y
    {
      get
      {
        return _y;
      }
      set
      {
        __isset.y = true;
        this._y = value;
      }
    }

    public double Width
    {
      get
      {
        return _width;
      }
      set
      {
        __isset.width = true;
        this._width = value;
      }
    }

    public double Height
    {
      get
      {
        return _height;
      }
      set
      {
        __isset.height = true;
        this._height = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool x;
      public bool y;
      public bool width;
      public bool height;
    }

    public ElifImageDetection() {
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
            if (field.Type == TType.Double) {
              X = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.Double) {
              Y = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.Double) {
              Width = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.Double) {
              Height = iprot.ReadDouble();
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
      TStruct struc = new TStruct("ElifImageDetection");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.x) {
        field.Name = "x";
        field.Type = TType.Double;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(X);
        oprot.WriteFieldEnd();
      }
      if (__isset.y) {
        field.Name = "y";
        field.Type = TType.Double;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Y);
        oprot.WriteFieldEnd();
      }
      if (__isset.width) {
        field.Name = "width";
        field.Type = TType.Double;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Width);
        oprot.WriteFieldEnd();
      }
      if (__isset.height) {
        field.Name = "height";
        field.Type = TType.Double;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Height);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("ElifImageDetection(");
      bool __first = true;
      if (__isset.x) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("X: ");
        __sb.Append(X);
      }
      if (__isset.y) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Y: ");
        __sb.Append(Y);
      }
      if (__isset.width) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Width: ");
        __sb.Append(Width);
      }
      if (__isset.height) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Height: ");
        __sb.Append(Height);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}

using System;
using System.Buffers;
using System.IO;
using System.Reflection;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Fantasy.Helper
{
    /// <summary>
    /// �ṩProtoBuf���л��ͷ����л��İ��������ࡣ
    /// </summary>
    public static class ProtoBufHelper
    {
        /// <summary>
        /// �� Span/byte �з����л�����
        /// </summary>
        /// <param name="type">Ҫ�����л��Ķ������͡�</param>
        /// <param name="span">Ҫ�����л����ֽ����ݡ�</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static object FromSpan(Type type, Span<byte> span)
        {
#if FANTASY_UNITY
            using var recyclableMemoryStream = MemoryStreamHelper.GetRecyclableMemoryStream();
            recyclableMemoryStream.Write(span);
            recyclableMemoryStream.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize(type, recyclableMemoryStream);
#else
            return RuntimeTypeModel.Default.Deserialize(type, span);
#endif
        }

        /// <summary>
        /// �� Memory/byte �з����л�����
        /// </summary>
        /// <param name="type">Ҫ�����л��Ķ������͡�</param>
        /// <param name="memory">Ҫ�����л����ڴ����ݡ�</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static object FromMemory(Type type, Memory<byte> memory)
        {
#if FANTASY_UNITY
            using var recyclableMemoryStream = MemoryStreamHelper.GetRecyclableMemoryStream();
            recyclableMemoryStream.Write(memory.Span);
            recyclableMemoryStream.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize(type, recyclableMemoryStream);
#else
            return RuntimeTypeModel.Default.Deserialize(type, memory);
#endif
        }

        /// <summary>
        /// ��ָ�����ֽ������е�ָ����Χ�����л�����
        /// </summary>
        /// <param name="type">Ҫ�����л��Ķ������͡�</param>
        /// <param name="bytes">�����������л����ݵ��ֽ����顣</param>
        /// <param name="index">Ҫ�����л����ݵ���ʼ������</param>
        /// <param name="count">Ҫ�����л����ֽ����ݳ��ȡ�</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static object FromBytes(Type type, byte[] bytes, int index, int count)
        {
#if FANTASY_UNITY
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            stream.Write(bytes, index, count);
            stream.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize(type, stream);
#else
            var memory = new Memory<byte>(bytes, index, count);
            return RuntimeTypeModel.Default.Deserialize(type, memory);
#endif
        }

        /// <summary>
        /// ���ֽ������з����л�����
        /// </summary>
        /// <typeparam name="T">Ҫ�����л��Ķ������͡�</typeparam>
        /// <param name="bytes">�����������л����ݵ��ֽ����顣</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static T FromBytes<T>(byte[] bytes)
        {
#if FANTASY_UNITY
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize<T>(stream);
#else
            return Serializer.Deserialize<T>(new Span<byte>(bytes));
#endif
        }

        /// <summary>
        /// ��ָ�����ֽ������е�ָ����Χ�����л�����
        /// </summary>
        /// <typeparam name="T">Ҫ�����л��Ķ������͡�</typeparam>
        /// <param name="bytes">�����������л����ݵ��ֽ����顣</param>
        /// <param name="index">Ҫ�����л����ݵ���ʼ������</param>
        /// <param name="count">Ҫ�����л����ֽ����ݳ��ȡ�</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static T FromBytes<T>(byte[] bytes, int index, int count)
        {
#if FANTASY_UNITY
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return Serializer.Deserialize<T>(stream);
#else
            return Serializer.Deserialize<T>(new Span<byte>(bytes, index, count));
#endif
        }

        /// <summary>
        /// ���������л�Ϊ�ֽ����顣
        /// </summary>
        /// <param name="message">Ҫ���л��Ķ���</param>
        /// <returns>�������л����ݵ��ֽ����顣</returns>
        public static byte[] ToBytes(object message)
        {
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            Serializer.Serialize(stream, message);
            return stream.ToArray();
        }

        /// <summary>
        /// ���������л���ָ�����ڴ��С�
        /// </summary>
        /// <param name="message">Ҫ���л��Ķ���</param>
        /// <param name="memory">Ŀ���ڴ档</param>
        public static void ToMemory(object message, Memory<byte> memory)
        {
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            Serializer.Serialize(stream, message);
            stream.GetBuffer().AsMemory().CopyTo(memory);
        }

        /// <summary>
        /// ���������л���ָ�������С�
        /// </summary>
        /// <param name="message">Ҫ���л��Ķ���</param>
        /// <param name="stream">Ŀ������</param>
        public static void ToStream(object message, MemoryStream stream)
        {
            Serializer.Serialize(stream, message);
        }

        /// <summary>
        /// ��ָ�������з����л�����
        /// </summary>
        /// <param name="type">Ҫ�����л��Ķ������͡�</param>
        /// <param name="stream">�����������л����ݵ�����</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static object FromStream(Type type, MemoryStream stream)
        {
            return Serializer.Deserialize(type, stream);
        }

        /// <summary>
        /// ��ָ�������з����л�����
        /// </summary>
        /// <typeparam name="T">Ҫ�����л��Ķ������͡�</typeparam>
        /// <param name="stream">�����������л����ݵ�����</param>
        /// <returns>�����л��õ��Ķ���</returns>
        public static T FromStream<T>(MemoryStream stream)
        {
            return (T) Serializer.Deserialize(typeof(T), stream);
        }

        /// <summary>
        /// ��¡һ������ͨ�����л��ͷ����л�ʵ����ȸ��ơ�
        /// </summary>
        /// <typeparam name="T">Ҫ��¡�Ķ������͡�</typeparam>
        /// <param name="t">Ҫ��¡�Ķ���</param>
        /// <returns>��¡����¶���</returns>
        public static T Clone<T>(T t)
        {
            using var stream = MemoryStreamHelper.GetRecyclableMemoryStream();
            Serializer.Serialize(stream, t);
            return Serializer.Deserialize<T>(stream);
        }
    }
}
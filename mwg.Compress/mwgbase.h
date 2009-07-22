#pragma once
#include <fstream>
#include <cstdlib>
#include <cstdio>
#include <errno.h>
#include <cmath>
#ifdef _MANAGED
#	pragma managed(push,off)
#endif
namespace mwg{
	typedef unsigned char		byte;
	typedef unsigned __int32	uint;
	typedef signed __int32		sint;
	typedef unsigned __int64	ulong;
	typedef signed __int64		slong;
	typedef int byte_n; // -1 ‚ÌŽž EOF

#ifndef NDEBUG
	template<typename TErr>
	static __declspec(noinline) void break_throw(const char* message){
		static int i=0;
		i++; // dummy –½—ß
		throw TErr(message);
	}
	static __declspec(noinline) void break_assert(bool condition){
		if(condition)return;
		static int i=0;
		i++; // dummy –½—ß
	}
#else
	template<typename TErr>
	static inline void break_throw(const char* message){throw TErr(message);}
	static inline void break_assert(){}
#	define break_assert(...) break_assert()
#endif
namespace Collections{
#ifdef _MANAGED
#	pragma managed
	template<typename T>
	ref class NativeList;

	template<typename T>
	ref class NativeList<T*>:System::Collections::Generic::List<System::IntPtr>{
		typedef System::Collections::Generic::List<System::IntPtr> base;
	public:
		property T* default[int]{
			T* get(int index) new{
				System::IntPtr value=((base^)this)[index];
				return (T*)(void*)value;
			}
			void set(int index,T* value){
				System::IntPtr v=(System::IntPtr)(void*)value;
				((base^)this)[index]=v;
			}
		}
		void Add(T* item){
			this->Add((System::IntPtr)item);
		}
		bool Contains(T* item){
			this->Contains((System::IntPtr)item);
		}
	};
#	pragma unmanaged
#endif
}
}
#ifdef _MANAGED
#	pragma managed(pop)
#endif
